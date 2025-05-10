using AD.Services.AppEvents;
using AD.Services.ProtectedTime;
using AD.ToolsCollection;

namespace Game.Instances
{
    public class ActiveInstanceModule : IAppTickListener
    {
        private readonly IInstancesService instancesService;
        private readonly InstancesState state;
        private readonly ITimeService time;

        public ActiveInstanceModule(IInstancesService instancesService, InstancesState state, ITimeService time)
        {
            this.instancesService = instancesService;
            this.state = state;
            this.time = time;
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

                if (completeTime >= time.TotalTime)
                {
                    continue;
                }

                var result = CalcResult(activeInstance);

                activeInstance.SetResult(result);
                activeInstance.MarAsReadyToComplete();

                state.DecrementGuaranteedCompleted();
            }
        }

        private CompleteResult CalcResult(ActiveInstanceInfo instance)
        {
            if (state.HasGuaranteedCompleted)
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