using AD.ToolsCollection;
using UnityEditor;

namespace Game.Guild
{
    [CustomPropertyDrawer(typeof(RoleId))]
    public class RoleIdProperty : KeyPropertyDrawer<RoleId>
    {
    }
}