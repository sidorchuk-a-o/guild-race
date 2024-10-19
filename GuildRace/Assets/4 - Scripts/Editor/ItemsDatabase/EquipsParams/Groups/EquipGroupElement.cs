using AD.ToolsCollection;
using System;

namespace Game.Items
{
    [KeyElement(typeof(EquipGroup))]
    public class EquipGroupElement : KeyElement
    {
        protected override Func<Collection<string>> GetCollection
        {
            get => ItemsDatabaseEditorState.GetEquipGroupsCollection;
        }

        protected override void CreateElementGUI(Element root)
        {
            base.CreateElementGUI(root);

            formatSelectedValueOn = false;
        }
    }
}