using System;
using AD.ToolsCollection;
using UnityEditor;

namespace AD.Game.Inventory
{
    [CustomPropertyDrawer(typeof(ItemsGridCellType))]
    public class ItemsGridCellTypeProperty : KeyPropertyDrawer<ItemsGridCellType, int>
    {
    }

    [KeyEditor(typeof(ItemsGridCellType))]
    public class ItemsGridCellTypeElement : KeyElement<int>
    {
        protected override Func<Collection<int>> GetCollection
        {
            get => InventoryEditorState.GetItemsGridCellTypesCollection;
        }
    }
}