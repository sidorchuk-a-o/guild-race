using System.Collections.Generic;
using AD.ToolsCollection;
using Game.Instances;
using UnityEngine.UIElements;

namespace Game.Quests
{
    [QuestsEditor(typeof(KillingBossesHandler))]
    public class KillingBossesHandlerEditor : QuestMechanicHandlerEditor
    {
        public override void CreateQuestParamsEditor(VisualElement root, List<string> questParams)
        {
            var instanceTypeId = questParams[0].IntParse();
            var instanceTypePopup = root.CreatePopup(InstancesEditorState.GetInstanceTypesViewCollection);

            instanceTypePopup.label = "Instance Type";
            instanceTypePopup.value = instanceTypeId;
        }
    }
}