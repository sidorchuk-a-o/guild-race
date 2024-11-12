using AD.ToolsCollection;
using System;

namespace Game.Guild
{
    [KeyElement(typeof(ClassId))]
    public class ClassIdElement : KeyElement<string>
    {
        protected override Func<Collection<string>> GetCollection
        {
            get => GuildEditorState.CreateClassesViewCollection;
        }

        protected override void CreateElementGUI(Element root)
        {
            base.CreateElementGUI(root);

            formatSelectedValueOn = false;
        }
    }
}