using AD.Services.Router;
using Cysharp.Threading.Tasks;
using System.Linq;

namespace Game.Instances
{
    public class InstancesVMFactory : VMFactory
    {
        private readonly IInstancesService instancesService;

        public InstancesVMFactory(IInstancesService instancesService)
        {
            this.instancesService = instancesService;
        }

        // == Season ==

        public SeasonVM GetFirstSeason()
        {
            var firstSeason = instancesService.Seasons.FirstOrDefault();

            return new SeasonVM(firstSeason);
        }

        // == Instance ==

        public InstanceVM GetInstance(int instanceId)
        {
            var instance = instancesService.Seasons.GetInstance(instanceId);

            return new InstanceVM(instance);
        }

        public InstanceVM GetCurrentInstance()
        {
            var instance = instancesService.CurrentInstance;

            return new InstanceVM(instance);
        }

        public async UniTask StartInstance(int instanceId)
        {
            await instancesService.StartInstance(instanceId);
        }

        public async UniTask StopCurrentInstance()
        {
            await instancesService.StopCurrentInstance();
        }

        // == Map == 

        public InstanceMapVM GetInstanceMap()
        {
            var mapComponent = InstanceMapComponent.GetComponent();

            return new InstanceMapVM(mapComponent);
        }
    }
}