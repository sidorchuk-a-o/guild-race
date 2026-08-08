using System.Collections.Generic;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Instances
{
    [DataEditor(typeof(ThreatConsumableHandler))]
    public class ThreatConsumableHandlerEditor : ConsumableMechanicHandlerEditor
    {
        public override void CreateParamsEditor(VisualElement root, List<string> questParams)
        {
            var threatId = questParams[0].IntParse();
            var threatIdPopup = root.CreatePopup(InstancesEditorState.GetThreatsViewCollection);

            threatIdPopup.LabelOn = false;
            threatIdPopup.Value = threatId;
        }
    }
}