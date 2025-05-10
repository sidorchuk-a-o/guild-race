using System.Collections.Generic;
using AD.ToolsCollection;
using Game.Instances;
using UnityEngine.UIElements;

namespace Game.Quests
{
    [QuestsEditor(typeof(CompleteInstanceHandler))]
    public class CompleteInstanceHandlerEditor : QuestMechanicHandlerEditor
    {
        public override void CreateQuestParamsEditor(VisualElement root, List<string> questParams)
        {
            var instanceId = questParams[0].IntParse();
            var instanceIdPopup = root.CreatePopup(InstancesEditorState.GetInstancesCollection);

            instanceIdPopup.label = "Instance";
            instanceIdPopup.value = instanceId;
        }
    }
}