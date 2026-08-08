using System;
using System.Collections.Generic;
using System.Linq;
using AD.Game.Guild;
using AD.Game.GuildLevels;
using AD.Game.Inventory;
using AD.Game.Weekly;
using AD.Management;
using AD.ProtectedTime;
using AD.Save;
using AD.ToolsCollection;
using ObservableCollections;
using R3;

namespace AD.Game.Instances
{
    public class InstancesState : ServiceState<InstancesStateSM>
    {
        private readonly InstancesConfig config;
        private readonly ITimeService time;
        private readonly IGuildService guildService;
        private readonly IGuildLevelsService guildLevelsService;
        private readonly IInventoryService inventoryService;
        private readonly IWeeklyService weeklyService;

        private readonly SeasonsCollection seasons = new(null);
        private readonly ActiveInstancesCollection activeInstances = new(null);
        private readonly List<UnitCooldownInfo> unitCooldowns = new();

        private readonly InstancesLevelContext levelContext;

        private int guaranteedCompletedCount;

        public override string SaveKey => "instances";
        public override SaveSource SaveSource => SaveSource.App;

        public ISeasonsCollection Seasons => seasons;
        public IActiveInstancesCollection ActiveInstances => activeInstances;
        public IReadOnlyList<UnitCooldownInfo> UnitCooldowns => unitCooldowns;
        public bool HasGuaranteedCompleted => guaranteedCompletedCount > 0;
        public int TotalCompletedCount { get; private set; }

        public ActiveInstanceInfo SetupInstance { get; private set; }
        public ActiveInstanceInfo CompletedInstance { get; private set; }

        public DateTime LastResetDay { get; private set; }
        public int LastResetWeek { get; private set; }

        public InstancesState(
            InstancesConfig config,
            ITimeService time,
            IGuildService guildService,
            IGuildLevelsService guildLevelsService,
            IInventoryService inventoryService,
            IWeeklyService weeklyService)
        {
            this.config = config;
            this.time = time;
            this.guildService = guildService;
            this.guildLevelsService = guildLevelsService;
            this.inventoryService = inventoryService;
            this.weeklyService = weeklyService;

            levelContext = new(config);
        }

        public override void Init()
        {
            base.Init();

            // guild levels
            guildLevelsService.RegisterContext(levelContext);

            levelContext.BossTriesMods.ForEach(x =>
            {
                UpgradeBossTries(x.Key, x.Value);
            });

            levelContext.BossTriesMods
                .ObserveReplace()
                .Subscribe(UpgradeBossTriesCallback);
        }

        // == Instances ==

        public void CreateSetupInstance(SetupInstanceArgs args)
        {
            var id = GuidUtils.Generate();
            var instance = seasons.GetInstance(args.InstanceId);
            var bossUnit = instance.GetBossUnit(args.BossUnitId);

            bossUnit.SetInstanceId(id);

            SetupInstance = new(id, instance, bossUnit, squad: null);
        }

        public void CancelSetupInstance()
        {
            SetupInstance = null;
        }

        public void CompleteSetupAndStartInstance()
        {
            activeInstances.Add(SetupInstance);

            SetupInstance.SetStartTime(time.TotalTime);
            SetupInstance = null;

            MarkAsDirty(true);
        }

        public void SetCompletedInstance(string activeInstanceId)
        {
            var activeInstance = activeInstances.GetInstance(activeInstanceId);

            CompletedInstance = activeInstance;
        }

        public int RemoveActiveInstance(string activeInstanceId)
        {
            var index = activeInstances.FindIndex(x =>
            {
                return x.Id == activeInstanceId;
            });

            activeInstances.RemoveAt(index);

            MarkAsDirty(true);

            return index;
        }

        public void IncreaseCompletedCount()
        {
            TotalCompletedCount++;
        }

        public void DecrementGuaranteedCompleted()
        {
            guaranteedCompletedCount--;

            MarkAsDirty(true);
        }

        public void SetResetDay(DateTime value)
        {
            LastResetDay = value;
        }

        public void SetResetWeek(int value)
        {
            LastResetWeek = value;
        }

        // == Guild Levels ==

        private void UpgradeBossTriesCallback(CollectionReplaceEvent<KeyValuePair<InstanceType, int>> args)
        {
            var instanceType = args.NewValue.Key;
            var increaseValue = args.NewValue.Value;

            UpgradeBossTries(instanceType, increaseValue);
        }

        private void UpgradeBossTries(InstanceType instanceType, int increaseValue)
        {
            var cooldown = unitCooldowns.First(x => x.InstanceType == instanceType);
            var newTries = cooldown.MaxTriesCount.CurrentValue + increaseValue;

            cooldown.SetMaxTriesCount(newTries);
        }

        public UnitCooldownInfo GetUnitCooldown(InstanceType instanceType)
        {
            return unitCooldowns.FirstOrDefault(x => x.InstanceType == instanceType);
        }

        // == Save ==

        protected override InstancesStateSM CreateSave()
        {
            var instancesStateSM = new InstancesStateSM
            {
                LastResetDay = LastResetDay,
                LastResetWeek = LastResetWeek,
                TotalCompletedCount = TotalCompletedCount,
                GuaranteedCompletedCount = guaranteedCompletedCount
            };

            instancesStateSM.SetSeasons(seasons);
            instancesStateSM.SetActiveInstances(activeInstances, inventoryService);

            return instancesStateSM;
        }

        protected override void CreateDefaultState()
        {
            unitCooldowns.AddRange(CreateDefaultUnitCooldowns());

            guaranteedCompletedCount = config.CompleteChanceParams.GuaranteedCompletedCount;
            seasons.AddRange(CreateDefaultSeasons());

            LastResetDay = DateTime.Now;
            LastResetWeek = weeklyService.CurrentWeek.CurrentValue;

            //CreateDefaultConsumables();
        }

        protected override void ReadSave(InstancesStateSM save)
        {
            LastResetDay = save.LastResetDay;
            LastResetWeek = save.LastResetWeek;
            TotalCompletedCount = save.TotalCompletedCount;
            guaranteedCompletedCount = save.GuaranteedCompletedCount;

            seasons.AddRange(save.GetSeasons(config));
            activeInstances.AddRange(save.GetActiveInstances(inventoryService, seasons));
            unitCooldowns.AddRange(CreateDefaultUnitCooldowns());

            CreateNewSeasons();
        }

        private IEnumerable<UnitCooldownInfo> CreateDefaultUnitCooldowns()
        {
            return config.UnitCooldownParams.Select(x =>
            {
                return new UnitCooldownInfo(x);
            });
        }

        private IEnumerable<SeasonInfo> CreateDefaultSeasons()
        {
            return config.Seasons.Select(CreateSeason);
        }

        private void CreateNewSeasons()
        {
            foreach (var seasonData in config.Seasons)
            {
                if (seasons.Any(x => x.Id == seasonData.Id))
                {
                    continue;
                }

                var season = CreateSeason(seasonData);

                seasons.Add(season);
            }
        }

        private static SeasonInfo CreateSeason(SeasonData seasonData)
        {
            var raidData = seasonData.Instances.FirstOrDefault(x => x.Type == InstanceTypes.Raid);
            var dungeonsData = seasonData.Instances.Where(x => x.Type == InstanceTypes.Dungeon);

            var raid = CreateInstance(raidData);
            var dungeons = dungeonsData.Select(CreateInstance);

            return new(seasonData, raid, dungeons);
        }

        private static InstanceInfo CreateInstance(InstanceData data)
        {
            var bossUnits = data.BoosUnits.Select(x => new UnitInfo(x));

            return new(data, bossUnits);
        }

        private void CreateDefaultConsumables()
        {
            var consumablesParams = config.ConsumablesParams;
            var consumablesCellTypes = consumablesParams.GridParams.CellTypes;

            var consumablesBank = guildService.BankTabs
                .Select(x => x.Grid)
                .First(x => consumablesCellTypes.Contains(x.CellType));

            var consumablesItems = consumablesParams.Items
                .Select(x => inventoryService.Factory.CreateItem(x))
                .OfType<ConsumablesItemInfo>();

            foreach (var consumables in consumablesItems)
            {
                consumables.Stack.SetValue(10);

                var placementArgs = new PlaceInPlacementArgs
                {
                    ItemId = consumables.Id,
                    PlacementId = consumablesBank.Id
                };

                inventoryService.TryPlaceItem(placementArgs);
            }
        }
    }
}