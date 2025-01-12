using AD.ToolsCollection;
using UnityEditor;

namespace Game.Instances
{
    [CustomPropertyDrawer(typeof(InstanceType))]
    public class InstanceTypeProperty : KeyPropertyDrawer<InstanceType, int>
    {
    }
}