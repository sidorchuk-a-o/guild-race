using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    [KeyElement(typeof(ItemsGridCellType))]
    public class ItemsGridCellTypeElement : KeyElement
    {
        protected override Func<Collection<string>> GetCollection
        {
            get => InventoryEditorState.GetItemsGridCellTypesCollection;
        }

        protected override void CreateElementGUI(Element root)
        {
            base.CreateElementGUI(root);

            formatSelectedValueOn = false;
        }
    }
}