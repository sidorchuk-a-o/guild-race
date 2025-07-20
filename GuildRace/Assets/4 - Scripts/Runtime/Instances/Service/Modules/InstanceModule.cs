using System;
using System.Collections.Generic;
using System.Linq;
using AD.Services.Router;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using Game.Guild;
using Game.Inventory;
using UniRx;
using UnityEngine;
using CharacterInfo = Game.Guild.CharacterInfo;

namespace Game.Instances
{
    public class InstanceModule
    {
        private readonly InstancesState state;

        private readonly GuildConfig guildConfig;
        private readonly InstancesConfig instancesConfig;

        private readonly IRouterService router;
        private readonly IGuildService guildService;
        private readonly IInventoryService inventoryService;
        private readonly IInstancesService instancesService;

        private readonly Subject<IEnumerable<RewardResult>> onRewardsReceived = new();
        private readonly Dictionary<string, CompositeDisp> unitBagDispCache = new();

        public IObservable<IEnumerable<RewardResult>> OnRewardsReceived => onRewardsReceived;

        public InstanceModule(
            InstancesState state,
            GuildConfig guildConfig,
            InstancesConfig instancesConfig,
            IRouterService router,
            IGuildService guildService,
            IInventoryService inventoryService,
            IInstancesService instancesService)
        {
            this.state = state;
            this.guildConfig = guildConfig;
            this.instancesConfig = instancesConfig;
            this.router = router;
            this.guildService = guildService;
            this.inventoryService = inventoryService;
            this.instancesService = instancesService;
        }

        public async UniTask StartSetupInstance(SetupInstanceArgs args)
        {
            // create
            state.CreateSetupInstance(args);

            // mark as dirty
            state.MarkAsDirty();
            guildService.StateMarkAsDirty();

            // setup
            await router.PushAsync(
                pathKey: RouteKeys.Instances.SetupInstance,
                loadingKey: LoadingScreenKeys.Loading);
        }

        public IReadOnlyCollection<SquadCandidateInfo> GetSquadCandidates()
        {
            var bossThreats = state.SetupInstance.BossUnit.Threats;

            var candidates = guildService.Characters.Select(character =>
            {
                var threats = GetCharacterThreats(character, bossThreats);

                return new SquadCandidateInfo(character.Id, threats);
            });

            return candidates.ToList();
        }

        private IEnumerable<ThreatInfo> GetCharacterThreats(CharacterInfo character, IReadOnlyCollection<ThreatId> bossThreats)
        {
            var specData = guildConfig.CharactersParams.GetSpecialization(character.SpecId);

            var threats = specData.Threats.Select(threat =>
            {
                var threatResolved = bossThreats.Contains(threat);
                var resolveCount = threatResolved ? 1 : 0;

                return new ThreatInfo(threat, resolveCount);
            });

            return threats;
        }

        public void TryAddCharacterToSquad(string characterId)
        {
            var setupInstance = state.SetupInstance;

            if (setupInstance == null)
            {
                return;
            }

            var instance = setupInstance.Instance;
            var squadParams = instancesConfig.SquadParams.GetSquadParams(instance.Type);

            if (setupInstance.Squad.Count >= squadParams.MaxUnitsCount)
            {
                return;
            }

            var setupInstanceId = setupInstance.Id;

            var character = guildService.Characters[characterId];
            var characterBag = inventoryService.Factory.CreateGrid(instancesConfig.SquadParams.Bag);

            var bossThreats = setupInstance.BossUnit.Threats;
            var resolvedThreats = GetCharacterThreats(character, bossThreats).Where(x => x.Resolved.Value);

            character.SetInstanceId(setupInstanceId);

            var squadUnit = new SquadUnitInfo(characterId, characterBag, resolvedThreats);

            setupInstance.AddUnit(squadUnit);

            UpdateThreats();
            UpdateCompleteChance();

            state.MarkAsDirty();

            // bag
            var unitBag = squadUnit.Bag;
            var unitBagDisp = new CompositeDisp();

            unitBag.Items
                .ObserveCountChanged()
                .Subscribe(UpdateCompleteChance)
                .AddTo(unitBagDisp);

            unitBagDispCache[unitBag.Id] = unitBagDisp;
        }

        public void TryRemoveCharacterFromSquad(string characterId)
        {
            var setupInstance = state.SetupInstance;

            if (setupInstance == null)
            {
                return;
            }

            var setupInstanceId = setupInstance.Id;
            var character = guildService.Characters[characterId];

            if (character.HasInstance == false ||
                character.InstanceId.Value != setupInstanceId)
            {
                return;
            }

            var squadUnit = setupInstance.Squad.FirstOrDefault(x => x.CharactedId == characterId);

            character.SetInstanceId(null);
            setupInstance.RemoveUnit(squadUnit);

            ResetUnitBag(squadUnit.Bag);

            UpdateThreats();
            UpdateCompleteChance();

            state.MarkAsDirty();
        }

        private void UpdateThreats()
        {
            var setupInstance = state.SetupInstance;

            var characters = setupInstance.Squad
                .Select(x => guildService.Characters[x.CharactedId]);

            var characterSpecs = characters
                .Select(x => x.SpecId)
                .Select(x => guildConfig.CharactersParams.GetSpecialization(x))
                .ToListPool();

            var subRoleThreats = characterSpecs
                .Select(x => guildConfig.CharactersParams.GetSubRoleThreat(x.SubRoleId));

            var specThreats = characterSpecs
                .SelectMany(x => x.Threats);

            var resolvedThreats = specThreats.Concat(subRoleThreats);

            setupInstance.ApplyResolveThreats(resolvedThreats);

            characterSpecs.ReleaseListPool();
        }

        private void UpdateCompleteChance()
        {
            var chance = CalcChance();
            var setupInstance = state.SetupInstance;

            setupInstance.SetCompleteChance(chance);
        }

        public float CalcChanceDiff(AddItemArgs args)
        {
            var setupInstance = state.SetupInstance;

            if (setupInstance == null)
            {
                return 0f;
            }

            var currentChance = setupInstance.CompleteChance.Value;

            var newChance = args.CharacterId.IsNullOrEmpty()
                ? CalcMaxSquadChance(args)
                : CalcChance(args);

            return newChance - currentChance;
        }

        private float CalcMaxSquadChance(AddItemArgs args)
        {
            if (state.SetupInstance.Squad.IsNullOrEmpty())
            {
                return 0f;
            }

            return state.SetupInstance.Squad.Max(x =>
            {
                return CalcChance(new AddItemArgs
                {
                    CharacterId = x.CharactedId,
                    ConsumablesId = args.ConsumablesId
                });
            });
        }

        private float CalcChance(AddItemArgs args = null)
        {
            var setupInstance = state.SetupInstance;
            var chanceParams = instancesConfig.CompleteChanceParams.GetParams(setupInstance.Instance.Type);

            // squad chance
            var squadCountChance = setupInstance.Squad.Count * chanceParams.CharactersCountMod;

            // threats chance
            var resolvedThreatsChance = setupInstance.Threats
                .Where(x => x.Resolved.Value)
                .Sum(calcResolvedThreatChance);

            float calcResolvedThreatChance(ThreatInfo threat)
            {
                var resolveCount = Mathf.Min(threat.ResolveCount, chanceParams.MaxResolveCount);
                var resolveChance = resolveCount * chanceParams.ResolveThreatMod;

                return resolveChance;
            }

            // squad data
            var healthMod = chanceParams.HealthMod;
            var powerMod = chanceParams.PowerMod;

            var squadData = setupInstance.Squad.Aggregate(new SquadChanceParams(setupInstance), (chanceParams, unit) =>
            {
                // character
                var character = guildService.Characters[unit.CharactedId];
                var specData = guildConfig.CharactersParams.GetSpecialization(character.SpecId);

                var specParams = specData.UnitParams;
                var equipItems = character.EquipSlots
                    .Where(x => x.HasItem)
                    .Select(x => x.Item.Value)
                    .OfType<EquipItemInfo>()
                    .ToListPool();

                var equipHealth = equipItems.Sum(x => x.CharacterParams.Health);
                var equipPower = equipItems.Sum(x => x.CharacterParams.Power);

                chanceParams.Health += specParams.Health + equipHealth;
                chanceParams.Power += specParams.Power + equipPower;

                equipItems.ReleaseListPool();

                // Consumables
                var consumableItems = unit.Bag.Items
                    .OfType<ConsumablesItemInfo>();

                foreach (var consumableItem in consumableItems)
                {
                    var mechanicId = consumableItem.MechanicId;
                    var mechanicHandler = instancesService.GetConsumableHandler(mechanicId);

                    mechanicHandler.Invoke(consumableItem, chanceParams);
                }

                // Args
                if (args != null && character.Id == args.CharacterId)
                {
                    var item = inventoryService.GetItem(args.ConsumablesId);
                    var consumableItem = item as ConsumablesItemInfo;

                    if (unit.Bag.CheckPossibilityOfPlacement(item))
                    {
                        var mechanicId = consumableItem.MechanicId;
                        var mechanicHandler = instancesService.GetConsumableHandler(mechanicId);

                        mechanicHandler.Invoke(consumableItem, chanceParams);
                    }
                }

                return chanceParams;
            });

            // health chance
            var bossHealth = setupInstance.BossUnit.UnitParams.Health;
            var healthChance = (int)((squadData.Health - bossHealth) / healthMod.CharactersMod) * healthMod.TotalMod;

            // power chance
            var bossPower = setupInstance.BossUnit.UnitParams.Power;
            var powerChance = (int)((squadData.Power - bossPower) / powerMod.CharactersMod) * powerMod.TotalMod;

            // chance
            return (squadCountChance + resolvedThreatsChance + healthChance + powerChance + squadData.ConsumableChance) / 100f;
        }

        public async UniTask CompleteSetupAndStartInstance()
        {
            var setupInstance = state.SetupInstance;

            if (setupInstance == null)
            {
                return;
            }

            // upd state
            state.CompleteSetupAndStartInstance();

            // mark as dirty
            guildService.StateMarkAsDirty();
            state.MarkAsDirty(true);

            // start
            await router.PushAsync(
                pathKey: RouteKeys.Hub.ActiveInstances,
                loadingKey: LoadingScreenKeys.Loading,
                parameters: RouteParams.FirstRoute);
        }

        public void CancelSetupInstance()
        {
            var setupInstance = state.SetupInstance;

            if (setupInstance == null)
            {
                return;
            }

            // reset instance
            ResetInstanceData(setupInstance);

            // revert items
            foreach (var squadUnit in setupInstance.Squad)
            {
                ResetUnitBag(squadUnit.Bag);
            }

            // cancel
            state.CancelSetupInstance();

            // mark as dirty
            guildService.StateMarkAsDirty();
            state.MarkAsDirty();
        }

        public int CompleteActiveInstance(string activeInstanceId)
        {
            // reset instance
            var activeInstance = state.ActiveInstances.GetInstance(activeInstanceId);

            ResetInstanceData(activeInstance);

            // rewards
            ReceiveRewards(activeInstance);

            // upd state
            state.SetCompletedInstance(activeInstanceId);

            return state.RemoveActiveInstance(activeInstanceId);
        }

        private void ResetInstanceData(ActiveInstanceInfo instance)
        {
            // reset characters
            foreach (var squadUnit in instance.Squad)
            {
                var character = guildService.Characters[squadUnit.CharactedId];

                character.SetInstanceId(null);
            }

            // reset boss
            instance.BossUnit.SetInstanceId(null);
        }

        private void ResetUnitBag(ItemsGridInfo bag)
        {
            if (unitBagDispCache.Remove(bag.Id, out var disp))
            {
                disp.Clear();
            }

            var bagCellType = instancesConfig.SquadParams.Bag.CellType;
            var guildTab = guildService.BankTabs.FirstOrDefault(x => x.Grid.CellType == bagCellType);

            foreach (var item in bag.Items)
            {
                inventoryService.TryPlaceItem(new PlaceInPlacementArgs
                {
                    ItemId = item.Id,
                    PlacementId = guildTab.Grid.Id
                });
            }
        }

        private void ReceiveRewards(ActiveInstanceInfo instance)
        {
            var rewardResults = new List<RewardResult>();
            var instanceResult = instance.Result.Value;

            if (instanceResult == CompleteResult.None)
            {
                return;
            }

            var bossId = instance.BossUnit.Id;
            var bossRewards = instancesConfig.GetUnitRewards(bossId);

            foreach (var rewardGroup in bossRewards.GroupBy(x => x.MechanicId))
            {
                var rewards = rewardGroup.ToListPool();
                var rewardHandler = instancesService.GetRewardHandler(rewardGroup.Key);

                var groupResult = rewardHandler.ApplyRewards(rewards, instance);

                rewardResults.AddRange(groupResult);
                rewards.ReleaseListPool();
            }

            if (rewardResults.Any())
            {
                instance.SetRewards(rewardResults);

                onRewardsReceived.OnNext(rewardResults);
            }
        }
    }
}