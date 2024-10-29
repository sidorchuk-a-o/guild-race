using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    [KeyElement(typeof(ItemsGridCategory))]
    public class ItemsGridCategoryElement : KeyElement
    {
        protected override Func<Collection<string>> GetCollection
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