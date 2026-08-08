using System.Collections.Generic;
using AD.Game.Instances;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Quests
{
    [DataEditor(typeof(CompleteInstanceHandler))]
    public class CompleteInstanceHandlerEditor : QuestMechanicHandlerEditor
    {
        public override void CreateQuestParamsEditor(VisualElement root, List<string> questParams)
        {
            var instanceId = questParams[0].IntParse();
            var instanceIdPopup = root.CreatePopup(InstancesEditorState.GetInstancesCollection);

            instanceIdPopup.Label = "Instance";
            instanceIdPopup.Value = instanceId;
        }
    }
}