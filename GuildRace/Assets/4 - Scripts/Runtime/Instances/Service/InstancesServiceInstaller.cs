using AD.DependencyInjection;
using AD.Services;

namespace Game.Instances
{
    [InstallerConfig(typeof(InstancesConfig))]
    public class InstancesServiceInstaller : ServiceInstaller<InstancesService, IInstancesService>
    {
    }
}