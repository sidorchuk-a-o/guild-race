using AD.DependencyInjection;
using AD.Services;

namespace Game.Items
{
    [InstallerConfig(typeof(ItemsDatabaseConfig))]
    public class ItemsDatabaseServiceInstaller : ServiceInstaller<ItemsDatabaseService, IItemsDatabaseService>
    {
    }
}