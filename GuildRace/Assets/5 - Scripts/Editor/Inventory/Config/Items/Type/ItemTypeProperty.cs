using System;
using AD.ToolsCollection;
using UnityEditor;

namespace AD.Game.Inventory
{
    [CustomPropertyDrawer(typeof(ItemType))]
    public class ItemTypeProperty : KeyPropertyDrawer<ItemType, int>
    {
    }

    [KeyEditor(typeof(ItemType))]
    public class ItemTypeElement : KeyElement<int>
    {
        protected override Func<Collection<int>> GetCollection
        {
            get => InventoryEditorState.CreateItemTypesCollection;
        }
    }
}