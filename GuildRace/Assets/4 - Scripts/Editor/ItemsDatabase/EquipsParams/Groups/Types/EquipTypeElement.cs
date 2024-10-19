using AD.ToolsCollection;
using System;

namespace Game.Items
{
    [KeyElement(typeof(EquipType))]
    public class EquipTypeElement : KeyElement
    {
        protected override Func<Collection<string>> GetCollection
        {
            get => ItemsDatabaseEditorState.GetEquipTypesCollection;
        }

        protected override void CreateElementGUI(Element root)
        {
            base.CreateElementGUI(root);

            formatSelectedValueOn = false;
        }
    }
}