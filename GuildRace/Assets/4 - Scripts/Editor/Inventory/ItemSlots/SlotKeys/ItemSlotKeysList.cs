using AD.ToolsCollection;

namespace Game.Inventory
{
    public class ItemSlotKeysList : ListElement<ItemSlot, ItemSlotKeyItem>
    {
        public override void BindData(SerializedData data)
        {
            headerTitle = "Item Slots";
            showCloneButton = false;

            base.BindData(data);
        }
    }
}