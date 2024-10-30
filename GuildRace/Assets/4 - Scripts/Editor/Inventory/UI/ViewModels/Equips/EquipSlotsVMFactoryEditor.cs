using AD.ToolsCollection;

namespace Game.Inventory
{
    [Menu("Equip Slots Factory")]
    [ItemSlotVMEditor(typeof(EquipSlotsVMFactory))]
    public class EquipSlotsVMFactoryEditor : ItemSlotsVMFactoryEditor
    {
    }
}