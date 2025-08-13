using System;
using System.Collections.Generic;
using System.Linq;
using AD.Services;
using AD.Services.AppEvents;
using AD.Services.Leaderboards;
using AD.Services.ProtectedTime;
using AD.Services.Router;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using Game.Guild;
using Game.Inventory;
using Game.Weekly;
using UniRx;
using VContainer;

namespace Game.Instances
{
    public class InstancesService : Service, IInstancesService
    {
        private readonly InstancesState state;

        private readonly InstanceModule instanceModule;
        private readonly ActiveInstanceModule activeInstanceModule;
        private readonly InstanceLeaderboardModule leaderboardModule;

        private readonly Dictionary<int, RewardHandler> rewardHandlers;
        private readonly Dictionary<int, ConsumableMechanicHandler> consumableHandlers;

        private readonly InstancesConfig instancesConfig;
        private readonly IWeeklyService weeklyService;
        private readonly IAppEventsService appEvents;
        private readonly IObjectResolver resolver;

        public ISeasonsCollection Seasons => state.Seasons;
        public IActiveInstancesCollection ActiveInstances => state.ActiveInstances;

        public ActiveInstanceInfo SetupInstance => state.SetupInstance;
        public ActiveInstanceInfo CompletedInstance => state.CompletedInstance;

        public IObservable<ActiveInstanceInfo> OnInstanceCompleted => activeInstanceModule.OnInstanceCompleted;
        public IObservable<IEnumerable<RewardResult>> OnRewardsReceived => instanceModule.OnRewardsReceived;

        public InstancesService(
            GuildConfig guildConfig,
            InstancesConfig instancesConfig,
            IGuildService guildService,
            IInventoryService inventoryService,
            IWeeklyService weeklyService,
            IRouterService router,
            IAppEventsService appEvents,
            ILeaderboardsService leaderboards,
            ITimeService time,
            IObjectResolver resolver)
        {
            this.instancesConfig = instancesConfig;
            this.weeklyService = weeklyService;
            this.appEvents = appEvents;
            this.resolver = resolver;

            state = new(instancesConfig, time, guildService, inventoryService, weeklyService, resolver);
            instanceModule = new(state, guildConfig, instancesConfig, router, guildService, inventoryService, this);
            activeInstanceModule = new(this, state, time);
            leaderboardModule = new(state, instancesConfig, activeInstanceModule, guildService, leaderboards, appEvents);

            rewardHandlers = instancesConfig.RewardsParams.RewardHandlers.ToDictionary(x => x.Id, x => x);
            consumableHandlers = instancesConfig.ConsumablesParams.MechanicHandlers.ToDictionary(x => x.Id, x => x);
        }

        public override async UniTask<bool> Init()
        {
            state.Init();

            InitHandlers();
            TryResetUnitsCooldown();

            leaderboardModule.Init();

            appEvents.AddAppTickListener(activeInstanceModule);

            return await Inited();
        }

        public void SetBossTimeState(bool state)
        {
            activeInstanceModule.SetBossTimeState(state);
        }

        // == Handlers ==

        private void InitHandlers()
        {
            rewardHandlers.Values.ForEach(handler =>
            {
                resolver.Inject(handler);
            });

            consumableHandlers.Values.ForEach(handler =>
            {
                resolver.Inject(handler);
            });
        }

        public RewardHandler GetRewardHandler(int id)
        {
            return rewardHandlers[id];
        }

        public ConsumableMechanicHandler GetConsumableHandler(int id)
        {
            return consumableHandlers[id];
        }

        // == Instance ==

        public async UniTask StartSetupInstance(SetupInstanceArgs args)
        {
            await instanceModule.StartSetupInstance(args);
        }

        public IReadOnlyCollection<SquadCandidateInfo> GetSquadCandidates()
        {
            return instanceModule.GetSquadCandidates();
        }

        public void TryAddCharacterToSquad(string characterId)
        {
            instanceModule.TryAddCharacterToSquad(characterId);
        }

        public void TryRemoveCharacterFromSquad(string characterId)
        {
            instanceModule.TryRemoveCharacterFromSquad(characterId);
        }

        public async UniTask CompleteSetupAndStartInstance()
        {
            await instanceModule.CompleteSetupAndStartInstance();
        }

        public void CancelSetupInstance()
        {
            instanceModule.CancelSetupInstance();
        }

        public void ForceReadyToCompleteActiveInstance(string activeInstanceId)
        {
            activeInstanceModule.MarkAsReadyToComplete(activeInstanceId);
        }

        public int CompleteActiveInstance(string activeInstanceId)
        {
            return instanceModule.CompleteActiveInstance(activeInstanceId);
        }

        public void ReceiveAdsRewards()
        {
            instanceModule.ReceiveAdsRewards();
        }

        public float CalcChanceDiff(AddItemArgs args)
        {
            return instanceModule.CalcChanceDiff(args);
        }

        // == Unit Cooldown ==

        private void TryResetUnitsCooldown()
        {
            foreach (var cooldownParams in instancesConfig.UnitCooldownParams)
            {
                if (cooldownParams.IsWeeklyReset)
                {
                    var currentWeek = weeklyService.CurrentWeek;
                    var lastResetWeek = state.LastResetWeek;

                    if (lastResetWeek == currentWeek)
                    {
                        continue;
                    }

                    state.SetResetWeek(currentWeek);
                }
                else
                {
                    var currentDate = DateTime.Today;
                    var lastResetData = state.LastResetDay;
                    var daysDelta = (currentDate - lastResetData).TotalDays;

                    if (daysDelta <= 0)
                    {
                        continue;
                    }

                    state.SetResetDay(currentDate);
                }

                var instanceType = cooldownParams.InstanceType;

                var bossUnits = state.Seasons
                    .SelectMany(x => x.GetInstances())
                    .Where(x => x.Type == instanceType)
                    .SelectMany(x => x.BossUnits);

                foreach (var bossUnit in bossUnits)
                {
                    bossUnit.ResetCompletedState();
                }
            }
        }

        public bool HasBossTries(int unitId)
        {
            var unit = Seasons.GetBossUnit(unitId);
            var instance = instancesConfig.GetBossInstance(unitId);
            var cooldownPatams = instancesConfig.GetUnitCooldown(instance.Type);

            var triesCount = unit.TriesCount.Value;
            var maxTriesCount = cooldownPatams.MaxTriesCount;

            return maxTriesCount < 0 || triesCount < maxTriesCount;
        }

        public bool HasBossComplete(int unitId)
        {
            var unit = Seasons.GetBossUnit(unitId);
            var instance = instancesConfig.GetBossInstance(unitId);
            var cooldownPatams = instancesConfig.GetUnitCooldown(instance.Type);

            var completedCount = unit.CompletedCount.Value;
            var maxCompletedCount = cooldownPatams.MaxCompletedCount;

            return maxCompletedCount < 0 || completedCount < maxCompletedCount;
        }

        public void AddTries(int unitId)
        {
            var unit = Seasons.GetBossUnit(unitId);

            unit.SetTriesCount(unit.TriesCount.Value - 1);

            state.MarkAsDirty();
        }

        // == Dispose ==

        public override void Dispose()
        {
            base.Dispose();

            CancelSetupInstance();

            appEvents.RemoveAppTickListener(activeInstanceModule);
        }
    }
}