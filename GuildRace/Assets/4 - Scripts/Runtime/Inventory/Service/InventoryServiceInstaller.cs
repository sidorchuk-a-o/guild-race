using AD.DependencyInjection;
using AD.Services;

namespace Game.Inventory
{
    [InstallerConfig(typeof(InventoryConfig))]
    public class InventoryServiceInstaller : ServiceInstaller<InventoryService, IInventoryService>
    {
    }
}