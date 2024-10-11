using AD.ToolsCollection;
using UnityEditor;

namespace Game.Guild
{
    [CustomPropertyDrawer(typeof(ClassId))]
    public class ClassIdProperty : KeyPropertyDrawer<ClassId>
    {
    }
}