using AD.ToolsCollection;
using UnityEditor;

namespace Game.Guild
{
    [CustomPropertyDrawer(typeof(ResourceId))]
    public class ResourceIdProperty : KeyPropertyDrawer<ResourceId, int>
    {
    }
}