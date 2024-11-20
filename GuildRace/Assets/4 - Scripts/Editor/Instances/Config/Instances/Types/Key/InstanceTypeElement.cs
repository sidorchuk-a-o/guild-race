using AD.ToolsCollection;
using System;

namespace Game.Instances
{
    [KeyElement(typeof(InstanceType))]
    public class InstanceTypeElement : KeyElement<int>
    {
        protected override Func<Collection<int>> GetCollection
        {
            get => InstancesEditorState.GetInstanceTypesViewCollection;
        }

        protected override void CreateElementGUI(Element root)
        {
            base.CreateElementGUI(root);

            formatSelectedValueOn = false;
        }
    }
}