using AD.Analytics;
using AD.AppEvents;
using AD.Management;
using AD.ProtectedTime;
using AD.ToolsCollection;

namespace AD.Game.Instances
{
    public class ActiveInstanceModule : ServiceModule, IAppTickHandler
    {
        private readonly InstancesState state;

        private readonly IInstancesService instancesService;
        private readonly IAnalyticsService analytics;
        private readonly ITimeService time;

        private readonly ReactiveEvent<ActiveInstanceInfo> onInstanceCompleted = new();

        private bool bossTimeOn = true;

        public ReadOnlyReactiveEvent<ActiveInstanceInfo> OnInstanceCompleted => onInstanceCompleted;

        public ActiveInstanceModule(
            InstancesState state,
            IInstancesService instancesService,
            IAnalyticsService analytics,
            ITimeService time)
        {
            this.state = state;
            this.instancesService = instancesService;
            this.analytics = analytics;
            this.time = time;
        }

        public void SetBossTimeState(bool state)
        {
            bossTimeOn = state;
        }

        void IAppTickHandler.OnTick(float deltaTime)
        {
            foreach (var activeInstance in instancesService.ActiveInstances)
            {
                if (activeInstance.IsReadyToComplete.CurrentValue)
                {
                    continue;
                }

                var startTime = activeInstance.StartTime.CurrentValue;
                var completeTime = startTime + activeInstance.BossUnit.CompleteTime;

#if UNITY_EDITOR || DEBUG_ENABLED
                if (completeTime >= time.TotalTime && bossTimeOn)
#else
                if (completeTime >= time.TotalTime)
#endif
                {
                    continue;
                }

                MarkAsReadyToComplete(activeInstance);
            }
        }

        internal void MarkAsReadyToComplete(string activeInstanceId)
        {
            var activeInstance = state.ActiveInstances.GetInstance(activeInstanceId);

            MarkAsReadyToComplete(activeInstance);
        }

        private void MarkAsReadyToComplete(ActiveInstanceInfo activeInstance)
        {
            var result = CalcResult(activeInstance);

            activeInstance.SetResult(result);
            activeInstance.MarkAsReadyToComplete();

            activeInstance.BossUnit.IncreaseTriesCount();

            if (activeInstance.Instance.Type == InstanceTypes.Dungeon)
            {
                state.DecrementGuaranteedCompleted();
            }

            if (result == CompleteResult.Completed)
            {
                state.IncreaseCompletedCount();
                activeInstance.BossUnit.IncreaseCompletedCount();

                onInstanceCompleted.OnNext(activeInstance);
            }

            analytics.CompleteInstance(activeInstance);
        }

        private CompleteResult CalcResult(ActiveInstanceInfo instance)
        {
            if (state.HasGuaranteedCompleted &&
                instance.Instance.Type == InstanceTypes.Dungeon)
            {
                return CompleteResult.Completed;
            }

            return RandUtils.CheckChance(instance.CompleteChance.CurrentValue) switch
            {
                true => CompleteResult.Completed,
                false => CompleteResult.Failed
            };
        }
    }
}