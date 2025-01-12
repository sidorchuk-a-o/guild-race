using AD.ToolsCollection;
using System;

namespace Game.Guild
{
    [KeyElement(typeof(SubRoleId))]
    public class SubRoleIdElement : KeyElement<int>
    {
        protected override Func<Collection<int>> GetCollection
        {
            get => GuildEditorState.CreateSubRolesViewCollection;
        }

        protected override void CreateElementGUI(Element root)
        {
            base.CreateElementGUI(root);

            formatSelectedValueOn = false;
        }
    }
}