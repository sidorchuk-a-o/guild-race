using System;
using AD.Services.AppEvents;
using AD.Services.ProtectedTime;
using AD.ToolsCollection;
using UniRx;

namespace Game.Instances
{
    public class ActiveInstanceModule : IAppTickListener
    {
        private readonly IInstancesService instancesService;
        private readonly InstancesState state;
        private readonly ITimeService time;

        private readonly Subject<ActiveInstanceInfo> onInstanceCompleted = new();

        private bool bossTimeOn = true;

        public IObservable<ActiveInstanceInfo> OnInstanceCompleted => onInstanceCompleted;

        public ActiveInstanceModule(
            IInstancesService instancesService,
            InstancesState state,
            ITimeService time)
        {
            this.instancesService = instancesService;
            this.state = state;
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

                var startTime = activeInstance.StartTime;
                var completeTime = startTime + activeInstance.BossUnit.CompleteTime;

#if UNITY_EDITOR || DEBUG_ENABLED
                if (completeTime >= time.TotalTime && bossTimeOn)
#else
                if (completeTime >= time.TotalTime)
#endif
                {
                    continue;
                }

                var result = CalcResult(activeInstance);

                activeInstance.SetResult(result);
                activeInstance.MarkAsReadyToComplete();

                activeInstance.BossUnit.IncreaseCompletedCount();

                if (activeInstance.Instance.Type == InstanceTypes.dungeon)
                {
                    state.DecrementGuaranteedCompleted();
                }

                if (result == CompleteResult.Completed)
                {
                    onInstanceCompleted.OnNext(activeInstance);
                }
            }
        }

        private CompleteResult CalcResult(ActiveInstanceInfo instance)
        {
            if (state.HasGuaranteedCompleted &&
                instance.Instance.Type == InstanceTypes.dungeon)
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