using System.Collections.Generic;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Instances
{
    [InstancesEditor(typeof(ThreatConsumableHandler))]
    public class ThreatConsumableHandlerEditor : ConsumableMechanicHandlerEditor
    {
        public override void CreateParamsEditor(VisualElement root, List<string> questParams)
        {
            var threatId = questParams[0].IntParse();
            var threataIdPopup = root.CreatePopup(InstancesEditorState.GetThreatsViewCollection, false);

            threataIdPopup.labelOn = false;
            threataIdPopup.value = threatId;
        }
    }
}