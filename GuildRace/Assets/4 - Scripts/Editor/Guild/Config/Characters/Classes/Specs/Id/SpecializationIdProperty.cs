using AD.ToolsCollection;
using UnityEditor;

namespace Game.Guild
{
    [CustomPropertyDrawer(typeof(SpecializationId))]
    public class SpecializationIdProperty : KeyPropertyDrawer<SpecializationId>
    {
    }
}