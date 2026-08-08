using System;
using System.Collections.Generic;
using System.Linq;
using AD.AppEvents;
using AD.Game.Weekly;
using AD.Management;
using AD.Review;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using VContainer;

namespace AD.Game.Instances
{
    public class InstancesService : Service, IInstancesService
    {
        private readonly InstancesState state;

        private readonly Dictionary<int, RewardHandler> rewardHandlers;
        private readonly Dictionary<int, ConsumableMechanicHandler> consumableHandlers;

        private readonly InstancesConfig instancesConfig;
        private readonly IWeeklyService weeklyService;
        private readonly IAppEventsService appEvents;
        private readonly IReviewService review;
        private readonly IObjectResolver resolver;

        private InstanceModule instanceModule;
        private ActiveInstanceModule activeInstanceModule;
        private InstanceLeaderboardModule leaderboardModule;

        public ISeasonsCollection Seasons => state.Seasons;
        public IActiveInstancesCollection ActiveInstances => state.ActiveInstances;

        public ActiveInstanceInfo SetupInstance => state.SetupInstance;
        public ActiveInstanceInfo CompletedInstance => state.CompletedInstance;

        public ReadOnlyReactiveEvent<ActiveInstanceInfo> OnInstanceCompleted => activeInstanceModule.OnInstanceCompleted;
        public ReadOnlyReactiveEvent<IEnumerable<RewardResult>> OnRewardsReceived => instanceModule.OnRewardsReceived;

        public InstancesService(
            InstancesConfig instancesConfig,
            InstancesState state,
            IWeeklyService weeklyService,
            IAppEventsService appEvents,
            IReviewService review,
            IObjectResolver resolver)
        {
            this.instancesConfig = instancesConfig;
            this.state = state;
            this.weeklyService = weeklyService;
            this.appEvents = appEvents;
            this.review = review;
            this.resolver = resolver;

            rewardHandlers = instancesConfig.RewardsParams.RewardHandlers.ToDictionary(x => x.Id, x => x);
            consumableHandlers = instancesConfig.ConsumablesParams.MechanicHandlers.ToDictionary(x => x.Id, x => x);
        }

        public override async UniTask<bool> Init()
        {
            instanceModule = ResolveModule<InstanceModule>();
            activeInstanceModule = ResolveModule<ActiveInstanceModule>();
            leaderboardModule = ResolveModule<InstanceLeaderboardModule>();

            InitHandlers();
            TryResetUnitsCooldown();

            leaderboardModule.Init();

            appEvents.AddAppTickHandler(activeInstanceModule);

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
            // instance
            var instance = state.ActiveInstances.GetInstance(activeInstanceId);
            var instanceResult = instance.Result.CurrentValue;

            // complete
            var index = instanceModule.CompleteActiveInstance(activeInstanceId);

            // review
            var completedCount = state.TotalCompletedCount;

            if (completedCount is 7 or 14 or 28 && instanceResult is CompleteResult.Completed)
            {
                review.ReviewShow();
            }

            return index;
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
            // weekly reset
            var currentWeek = weeklyService.CurrentWeek.CurrentValue;
            var lastResetWeek = state.LastResetWeek;

            if (lastResetWeek != currentWeek)
            {
                resetCooldowns(x => x.IsWeeklyReset);

                state.SetResetWeek(currentWeek);
            }

            // daily reset
            var currentDate = DateTime.Today;
            var lastResetData = state.LastResetDay;
            var daysDelta = (currentDate - lastResetData).TotalDays;

            if (daysDelta > 0)
            {
                resetCooldowns(x => !x.IsWeeklyReset);

                state.SetResetDay(currentDate);
            }

            return;
            // reset method
            void resetCooldowns(Func<UnitCooldownInfo, bool> filter)
            {
                foreach (var cooldownParams in state.UnitCooldowns.Where(filter))
                {
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
        }

        public bool HasBossTries(int unitId)
        {
            var unit = Seasons.GetBossUnit(unitId);
            var instance = instancesConfig.GetBossInstance(unitId);
            var cooldownParams = GetUnitCooldown(instance.Type);

            var triesCount = unit.TriesCount.CurrentValue;
            var maxTriesCount = cooldownParams.MaxTriesCount.CurrentValue;

            return maxTriesCount < 0 || triesCount < maxTriesCount;
        }

        public bool HasBossComplete(int unitId)
        {
            var unit = Seasons.GetBossUnit(unitId);
            var instance = instancesConfig.GetBossInstance(unitId);
            var cooldownParams = GetUnitCooldown(instance.Type);

            var completedCount = unit.CompletedCount.CurrentValue;
            var maxCompletedCount = cooldownParams.MaxCompletedCount;

            return maxCompletedCount < 0 || completedCount < maxCompletedCount;
        }

        public UnitCooldownInfo GetUnitCooldown(InstanceType type)
        {
            return state.GetUnitCooldown(type);
        }

        public void AddTries(int unitId)
        {
            var unit = Seasons.GetBossUnit(unitId);

            unit.SetTriesCount(unit.TriesCount.CurrentValue - 1);

            state.MarkAsDirty();
        }

        // == Dispose ==

        public override void Dispose()
        {
            base.Dispose();

            CancelSetupInstance();

            appEvents.RemoveAppTickHandler(activeInstanceModule);
        }
    }
}