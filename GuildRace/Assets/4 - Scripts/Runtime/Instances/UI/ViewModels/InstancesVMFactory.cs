using AD.Services.Router;
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

        public SeasonVM GetFirstSeason()
        {
            var firstSeason = instancesService.Seasons.FirstOrDefault();

            return new SeasonVM(firstSeason);
        }

        public InstanceVM GetInstance(int seasonId, int instanceId)
        {
            var season = instancesService.Seasons.GetById(seasonId);
            var instance = season.GetInstanceById(instanceId);

            return new InstanceVM(instance);
        }
    }
}