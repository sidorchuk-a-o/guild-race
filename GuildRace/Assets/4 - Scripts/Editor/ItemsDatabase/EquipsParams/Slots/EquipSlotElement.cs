using AD.ToolsCollection;
using System;

namespace Game.Items
{
    [KeyElement(typeof(EquipSlot))]
    public class EquipSlotElement : KeyElement
    {
        protected override Func<Collection<string>> GetCollection
        {
            get => ItemsDatabaseEditorState.GetEquipSlotsCollection;
        }

        protected override void CreateElementGUI(Element root)
        {
            base.CreateElementGUI(root);

            formatSelectedValueOn = false;
        }
    }
}