using System.Collections.Generic;
using AD.Game.Instances;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Quests
{
    [DataEditor(typeof(KillingBossesHandler))]
    public class KillingBossesHandlerEditor : QuestMechanicHandlerEditor
    {
        public override void CreateQuestParamsEditor(VisualElement root, List<string> questParams)
        {
            var instanceTypeId = questParams[0].IntParse();
            var instanceTypePopup = root.CreatePopup(InstancesEditorState.GetInstanceTypesViewCollection);

            instanceTypePopup.Label = "Instance Type";
            instanceTypePopup.Value = instanceTypeId;
        }
    }
}