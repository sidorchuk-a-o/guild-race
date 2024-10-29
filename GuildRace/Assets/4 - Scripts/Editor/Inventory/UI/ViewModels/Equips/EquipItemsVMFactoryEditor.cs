using AD.ToolsCollection;

namespace Game.Inventory
{
    [Menu("Equip Items Factory")]
    [ItemVMEditor(typeof(EquipItemsVMFactory))]
    public class EquipItemsVMFactoryEditor : ItemsVMFactoryEditor
    {
    }
}