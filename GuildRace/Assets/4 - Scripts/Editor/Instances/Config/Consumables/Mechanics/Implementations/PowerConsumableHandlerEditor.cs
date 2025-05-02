using System.Collections.Generic;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Instances
{
    [InstancesEditor(typeof(PowerConsumableHandler))]
    public class PowerConsumableHandlerEditor : ConsumableMechanicHandlerEditor
    {
        public override void CreateParamsEditor(VisualElement root, List<string> questParams)
        {
            var value = questParams[0].IntParse();
            var valueLabel = root.CreateElement<LabelElement>();

            valueLabel.text = value.ToString();
        }
    }
}