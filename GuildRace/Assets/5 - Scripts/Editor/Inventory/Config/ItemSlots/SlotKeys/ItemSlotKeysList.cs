using System;
using AD.ToolsCollection;

namespace AD.Game.Inventory
{
    public class ItemSlotKeysList : ListElement<ItemSlot>
    {
        protected override Type ItemType { get; } = typeof(ItemSlotKeyItem);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            HeaderTitle = "Item Slots";
            ShowCloneButton = false;
        }
    }
}