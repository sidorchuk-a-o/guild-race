using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    [KeyElement(typeof(ItemSlot))]
    public class ItemSlotKeyElement : KeyElement
    {
        protected override Func<Collection<string>> GetCollection
        {
            get => InventoryEditorState.GetItemSlotsCollection;
        }

        protected override void CreateElementGUI(Element root)
        {
            base.CreateElementGUI(root);

            formatSelectedValueOn = false;
        }
    }
}