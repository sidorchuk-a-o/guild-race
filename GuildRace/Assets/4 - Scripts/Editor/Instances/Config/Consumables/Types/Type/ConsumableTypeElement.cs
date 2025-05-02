using AD.ToolsCollection;
using System;

namespace Game.Instances
{
    [KeyElement(typeof(ConsumableType))]
    public class ConsumableTypeElement : KeyElement<int>
    {
        protected override Func<Collection<int>> GetCollection
        {
            get => InstancesEditorState.GetConsumableTypesViewCollection;
        }

        protected override void CreateElementGUI(Element root)
        {
            base.CreateElementGUI(root);

            formatSelectedValueOn = false;
        }
    }
}