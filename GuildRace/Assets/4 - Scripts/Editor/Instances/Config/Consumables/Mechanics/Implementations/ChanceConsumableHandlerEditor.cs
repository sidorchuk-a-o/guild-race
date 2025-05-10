using System.Collections.Generic;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Instances
{
    [InstancesEditor(typeof(ChanceConsumableHandler))]
    public class ChanceConsumableHandlerEditor : ConsumableMechanicHandlerEditor
    {
        public override void CreateParamsEditor(VisualElement root, List<string> questParams)
        {
            var chance = questParams[0].IntParse();
            var chanceLabel = root.CreateElement<LabelElement>();

            chanceLabel.text = chance.ToString();
        }
    }
}