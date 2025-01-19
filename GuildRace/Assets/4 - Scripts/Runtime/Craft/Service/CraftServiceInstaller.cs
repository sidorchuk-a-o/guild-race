using AD.DependencyInjection;
using AD.Services;

namespace Game.Craft
{
    [InstallerConfig(typeof(CraftConfig))]
    public class CraftServiceInstaller : ServiceInstaller<CraftService, ICraftService>
    {
    }
}