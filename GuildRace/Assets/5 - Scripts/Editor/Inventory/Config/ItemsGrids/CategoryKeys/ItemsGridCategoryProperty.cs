using System;
using AD.ToolsCollection;
using UnityEditor;

namespace AD.Game.Inventory
{
    [CustomPropertyDrawer(typeof(ItemsGridCategory))]
    public class ItemsGridCategoryProperty : KeyPropertyDrawer<ItemsGridCategory, int>
    {
    }

    [KeyEditor(typeof(ItemsGridCategory))]
    public class ItemsGridCategoryElement : KeyElement<int>
    {
        protected override Func<Collection<int>> GetCollection
        {
            get => InventoryEditorState.GetItemsGridCategoriesCollection;
        }
    }
}