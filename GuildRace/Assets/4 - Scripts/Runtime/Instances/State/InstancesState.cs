using AD.Services;
using AD.Services.ProtectedTime;
using AD.Services.Save;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using Game.Guild;
using Game.Inventory;
using Game.Weekly;
using System;
using System.Linq;
using System.Collections.Generic;
using VContainer;

namespace Game.Instances
{
    public class InstancesState : ServiceState<InstancesConfig, InstancesStateSM>
    {
        private readonly ITimeService time;
        private readonly IGuildService guildService;
        private readonly IInventoryService inventoryService;
        private readonly IWeeklyService weeklyService;

        private readonly SeasonsCollection seasons = new(null);
        private readonly ActiveInstancesCollection activeInstances = new(null);
        private int guaranteedCompletedCount;

        public override string SaveKey => InstancesStateSM.key;
        public override SaveSource SaveSource => SaveSource.app;

        public ISeasonsCollection Seasons => seasons;
        public IActiveInstancesCollection ActiveInstances => activeInstances;
        public bool HasGuaranteedCompleted => guaranteedCompletedCount > 0;

        public ActiveInstanceInfo SetupInstance { get; private set; }
        public ActiveInstanceInfo CompletedInstance { get; private set; }

        public DateTime LastResetDay { get; private set; }
        public int LastResetWeek { get; private set; }

        public InstancesState(
            InstancesConfig config,
            ITimeService time,
            IGuildService guildService,
            IInventoryService inventoryService,
            IWeeklyService weeklyService,
            IObjectResolver resolver)
            : base(config, resolver)
        {
            this.time = time;
            this.guildService = guildService;
            this.inventoryService = inventoryService;
            this.weeklyService = weeklyService;
        }

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

            var activeInstance = activeInstances[index];

            activeInstances.RemoveAt(index);

            MarkAsDirty(true);

            return index;
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

        // == Save ==

        protected override InstancesStateSM CreateSave()
        {
            var instancesStateSM = new InstancesStateSM
            {
                LastResetDay = LastResetDay,
                LastResetWeek = LastResetWeek,
                GuaranteedCompletedCount = guaranteedCompletedCount
            };

            instancesStateSM.SetSeasons(seasons);
            instancesStateSM.SetActiveInstances(activeInstances, inventoryService);

            return instancesStateSM;
        }

        protected override void ReadSave(InstancesStateSM save)
        {
            if (save == null)
            {
                guaranteedCompletedCount = config.CompleteChanceParams.GuaranteedCompletedCount;
                seasons.AddRange(CreateDefaultSeasons());

                LastResetDay = DateTime.Now;
                LastResetWeek = weeklyService.CurrentWeek;

                //CreateDefaultConsumables();

                return;
            }

            LastResetDay = save.LastResetDay;
            LastResetWeek = save.LastResetWeek;
            guaranteedCompletedCount = save.GuaranteedCompletedCount;

            seasons.AddRange(save.GetSeasons(config));
            activeInstances.AddRange(save.GetActiveInstances(inventoryService, seasons));

            CreateNewSeasons();
        }

        private IEnumerable<SeasonInfo> CreateDefaultSeasons()
        {
            return config.Seasons.Select(seasonData =>
            {
                return CreateSeason(seasonData);
            });
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
            var dungeons = dungeonsData.Select(x => CreateInstance(x));

            return new SeasonInfo(seasonData, raid, dungeons);
        }

        private static InstanceInfo CreateInstance(InstanceData data)
        {
            var bossUnits = data.BoosUnits.Select(x => new UnitInfo(x));

            return new InstanceInfo(data, bossUnits);
        }

        private void CreateDefaultConsumables()
        {
            var consumablesParams = config.ConsumablesParams;
            var consumablesCellTypes = consumablesParams.GridParams.CellTypes;

            var consumablesBank = guildService.BankTabs
                .Select(x => x.Grid)
                .FirstOrDefault(x => consumablesCellTypes.Contains(x.CellType));

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