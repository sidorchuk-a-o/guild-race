using System;
using AD.ToolsCollection;
using UnityEditor;

namespace AD.Game.Inventory
{
    [CustomPropertyDrawer(typeof(ItemSlot))]
    public class ItemSlotKeyProperty : KeyPropertyDrawer<ItemSlot, int>
    {
    }

    [KeyEditor(typeof(ItemSlot))]
    public class ItemSlotKeyElement : KeyElement<int>
    {
        protected override Func<Collection<int>> GetCollection
        {
            get => InventoryEditorState.GetItemSlotsCollection;
        }
    }
}