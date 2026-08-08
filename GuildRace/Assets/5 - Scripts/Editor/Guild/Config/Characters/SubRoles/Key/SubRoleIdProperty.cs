using System;
using AD.ToolsCollection;
using UnityEditor;

namespace AD.Game.Guild
{
    [CustomPropertyDrawer(typeof(SubRoleId))]
    public class SubRoleIdProperty : KeyPropertyDrawer<SubRoleId, int>
    {
    }

    [KeyEditor(typeof(SubRoleId))]
    public class SubRoleIdElement : KeyElement<int>
    {
        protected override Func<Collection<int>> GetCollection
        {
            get => GuildEditorState.CreateSubRolesViewCollection;
        }
    }
}