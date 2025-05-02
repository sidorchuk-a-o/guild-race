using System.Collections.Generic;
using AD.ToolsCollection;
using Game.Instances;
using UnityEngine.UIElements;

namespace Game.Quests
{
    [QuestsEditor(typeof(KillingBossHandler))]
    public class KillingBossHandlerEditor : QuestMechanicHandlerEditor
    {
        public override void CreateQuestParamsEditor(VisualElement root, List<string> questParams)
        {
            var unitId = questParams[0].IntParse();
            var unitIdPopup = root.CreatePopup(InstancesEditorState.GetBossesCollection);

            unitIdPopup.label = "Boss Unit";
            unitIdPopup.value = unitId;
        }
    }
}