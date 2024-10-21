using AD.DependencyInjection;
using AD.Services;

namespace Game.Items
{
    [InstallerConfig(typeof(ItemsConfig))]
    public class ItemsServiceInstaller : ServiceInstaller<ItemsService, IItemsService>
    {
    }
}