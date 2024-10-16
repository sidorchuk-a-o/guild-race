using AD.ToolsCollection;
using System;

namespace Game.Guild
{
    [KeyElement(typeof(SpecializationId))]
    public class SpecializationIdElement : KeyElement
    {
        protected override Func<Collection<string>> GetCollection
        {
            get => GuildEditorState.CreateSpecializationsViewCollection;
        }

        protected override void CreateElementGUI(Element root)
        {
            base.CreateElementGUI(root);

            formatSelectedValueOn = false;
        }
    }
}