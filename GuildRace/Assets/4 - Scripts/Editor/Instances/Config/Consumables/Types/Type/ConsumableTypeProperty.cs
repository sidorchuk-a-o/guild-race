using AD.ToolsCollection;
using UnityEditor;

namespace Game.Instances
{
    [CustomPropertyDrawer(typeof(ConsumableType))]
    public class ConsumableTypeProperty : KeyPropertyDrawer<ConsumableType, int>
    {
    }
}