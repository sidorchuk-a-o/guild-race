using AD.ToolsCollection;
using System;

namespace Game.Guild
{
    [KeyElement(typeof(SpecializationId))]
    public class SpecializationIdElement : KeyElement<int>
    {
        protected override Func<Collection<int>> GetCollection
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