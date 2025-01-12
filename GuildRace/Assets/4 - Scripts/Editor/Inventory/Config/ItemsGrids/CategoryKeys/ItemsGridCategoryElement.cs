using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    [KeyElement(typeof(ItemsGridCategory))]
    public class ItemsGridCategoryElement : KeyElement<int>
    {
        protected override Func<Collection<int>> GetCollection
        {
            get => InventoryEditorState.GetItemsGridCategoriesCollection;
        }

        protected override void CreateElementGUI(Element root)
        {
            base.CreateElementGUI(root);

            formatSelectedValueOn = false;
        }
    }
}