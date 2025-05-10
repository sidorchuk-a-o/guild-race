using AD.ToolsCollection;
using UnityEditor;

namespace Game.Instances
{
    [CustomPropertyDrawer(typeof(ThreatId))]
    public class ThreatIdProperty : KeyPropertyDrawer<ThreatId, int>
    {
    }
}