using AD.Services.AppEvents;
using AD.Services.ProtectedTime;

namespace Game.Instances
{
    public class ActiveInstanceModule : IAppTickListener
    {
        private readonly IInstancesService instancesService;
        private readonly ITimeService time;

        public ActiveInstanceModule(IInstancesService instancesService, ITimeService time)
        {
            this.instancesService = instancesService;
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

                activeInstance.MarAsReadyToComplete();
            }
        }

        void IAppTickListener.OnLateTick(float deltaTime)
        {
        }
    }
}