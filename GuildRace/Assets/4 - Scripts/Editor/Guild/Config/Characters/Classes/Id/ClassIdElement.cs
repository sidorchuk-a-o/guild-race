using AD.ToolsCollection;
using System;

namespace Game.Guild
{
    [KeyElement(typeof(ClassId))]
    public class ClassIdElement : KeyElement<int>
    {
        protected override Func<Collection<int>> GetCollection
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