using AD.Services.AppEvents;
using AD.Services.ProtectedTime;

namespace Game.Instances
{
    public class ActiveInstanceModule : IAppTickListener
    {
        private readonly ActiveInstanceParams activeInstanceParams;

        private readonly IInstancesService instancesService;
        private readonly ITimeService time;

        public ActiveInstanceModule(
            InstancesConfig instancesConfig,
            IInstancesService instancesService,
            ITimeService time)
        {
            this.instancesService = instancesService;
            this.time = time;

            activeInstanceParams = instancesConfig.ActiveInstanceParams;
        }

        void IAppTickListener.OnTick(float deltaTime)
        {
            foreach (var activeInstance in instancesService.ActiveInstances)
            {
                if (activeInstance.Equals(instancesService.PlayerInstance) ||
                    activeInstance.IsReadyToComplete.Value)
                {
                    continue;
                }

                var startTime = activeInstance.StartTime;
                var completeTime = startTime + activeInstanceParams.TempCompeteTime;

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