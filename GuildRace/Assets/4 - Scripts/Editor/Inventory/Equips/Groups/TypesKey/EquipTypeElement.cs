using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    [KeyElement(typeof(EquipType))]
    public class EquipTypeElement : KeyElement<int>
    {
        protected override Func<Collection<int>> GetCollection
        {
            get => InventoryEditorState.GetEquipTypesCollection;
        }

        protected override void CreateElementGUI(Element root)
        {
            base.CreateElementGUI(root);

            formatSelectedValueOn = false;
        }
    }
}