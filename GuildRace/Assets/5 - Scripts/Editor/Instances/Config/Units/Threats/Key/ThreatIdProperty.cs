using System;
using AD.ToolsCollection;
using UnityEditor;

namespace AD.Game.Instances
{
    [CustomPropertyDrawer(typeof(ThreatId))]
    public class ThreatIdProperty : KeyPropertyDrawer<ThreatId, int>
    {
    }

    [KeyEditor(typeof(ThreatId))]
    public class ThreatIdElement : KeyElement<int>
    {
        protected override Func<Collection<int>> GetCollection
        {
            get => InstancesEditorState.GetThreatsViewCollection;
        }
    }
}