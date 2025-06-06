using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    [KeyElement(typeof(ItemType))]
    public class ItemTypeElement : KeyElement<int>
    {
        protected override Func<Collection<int>> GetCollection
        {
            get => InventoryEditorState.CreateItemTypesCollection;
        }

        protected override void CreateElementGUI(Element root)
        {
            base.CreateElementGUI(root);

            formatSelectedValueOn = false;
        }
    }
}