using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    [KeyElement(typeof(EquipGroup))]
    public class EquipGroupElement : KeyElement<int>
    {
        protected override Func<Collection<int>> GetCollection
        {
            get => InventoryEditorState.GetEquipGroupsCollection;
        }

        protected override void CreateElementGUI(Element root)
        {
            base.CreateElementGUI(root);

            formatSelectedValueOn = false;
        }
    }
}