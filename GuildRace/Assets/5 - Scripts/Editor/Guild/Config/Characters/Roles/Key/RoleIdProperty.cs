using System;
using AD.ToolsCollection;
using UnityEditor;

namespace AD.Game.Guild
{
    [CustomPropertyDrawer(typeof(RoleId))]
    public class RoleIdProperty : KeyPropertyDrawer<RoleId, int>
    {
    }

    [KeyEditor(typeof(RoleId))]
    public class RoleIdElement : KeyElement<int>
    {
        protected override Func<Collection<int>> GetCollection
        {
            get => GuildEditorState.CreateRolesViewCollection;
        }
    }
}