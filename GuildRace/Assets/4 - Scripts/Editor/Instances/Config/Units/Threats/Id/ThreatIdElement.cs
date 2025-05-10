using AD.ToolsCollection;
using System;

namespace Game.Instances
{
    [KeyElement(typeof(ThreatId))]
    public class ThreatIdElement : KeyElement<int>
    {
        protected override Func<Collection<int>> GetCollection
        {
            get => InstancesEditorState.GetThreatsViewCollection;
        }

        protected override void CreateElementGUI(Element root)
        {
            base.CreateElementGUI(root);

            formatSelectedValueOn = false;
        }
    }
}