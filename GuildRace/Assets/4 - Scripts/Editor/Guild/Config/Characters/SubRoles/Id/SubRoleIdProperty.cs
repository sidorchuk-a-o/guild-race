using AD.ToolsCollection;
using UnityEditor;

namespace Game.Guild
{
    [CustomPropertyDrawer(typeof(SubRoleId))]
    public class SubRoleIdProperty : KeyPropertyDrawer<SubRoleId, int>
    {
    }
}