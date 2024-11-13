using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    [KeyElement(typeof(Rarity))]
    public class RarityElement : KeyElement<int>
    {
        protected override Func<Collection<int>> GetCollection
        {
            get => InventoryEditorState.GetRaritiesCollection;
        }

        protected override void CreateElementGUI(Element root)
        {
            base.CreateElementGUI(root);

            formatSelectedValueOn = false;
        }
    }
}