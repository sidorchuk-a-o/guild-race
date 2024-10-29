using AD.ToolsCollection;

namespace Game.Inventory
{
    public class ItemSlotsList : ListElement<ItemSlotData, ItemSlotItem>
    {
        public override void BindData(SerializedData data)
        {
            headerTitle = "Item Slots";
            wizardType = typeof(ItemSlotCreateWizard);

            showCloneButton = false;

            base.BindData(data);
        }
    }
}