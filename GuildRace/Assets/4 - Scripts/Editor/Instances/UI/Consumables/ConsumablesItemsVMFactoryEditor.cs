using AD.ToolsCollection;
using Game.Inventory;

namespace Game.Instances
{
    [Menu("Сonsumables Items Factory")]
    [ItemVMEditor(typeof(ConsumablesItemsVMFactory))]
    public class ConsumablesItemsVMFactoryEditor : ItemsVMFactoryEditor
    {
    }
}