using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    [KeyElement(typeof(Rarity))]
    public class RarityElement : KeyElement
    {
        protected override Func<Collection<string>> GetCollection
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