using System;
using AD.Services.Analytics;
using AD.Services.AppEvents;
using AD.Services.ProtectedTime;
using AD.ToolsCollection;
using UniRx;

namespace Game.Instances
{
    public class ActiveInstanceModule : IAppTickListener
    {
        private readonly InstancesState state;

        private readonly IInstancesService instancesService;
        private readonly IAnalyticsService analytics;
        private readonly ITimeService time;

        private readonly Subject<ActiveInstanceInfo> onInstanceCompleted = new();

        private bool bossTimeOn = true;

        public IObservable<ActiveInstanceInfo> OnInstanceCompleted => onInstanceCompleted;

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

        void IAppTickListener.OnTick(float deltaTime)
        {
            foreach (var activeInstance in instancesService.ActiveInstances)
            {
                if (activeInstance.IsReadyToComplete.Value)
                {
                    continue;
                }

                var startTime = activeInstance.StartTime.Value;
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

            return RandUtils.CheckChance(instance.CompleteChance.Value) switch
            {
                true => CompleteResult.Completed,
                false => CompleteResult.Failed
            };
        }

        void IAppTickListener.OnLateTick(float deltaTime)
        {
        }
    }
}