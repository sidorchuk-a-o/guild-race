using System.Collections.Generic;
using AD.Game.Instances;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Quests
{
    [DataEditor(typeof(KillingBossHandler))]
    public class KillingBossHandlerEditor : QuestMechanicHandlerEditor
    {
        public override void CreateQuestParamsEditor(VisualElement root, List<string> questParams)
        {
            var unitId = questParams[0].IntParse();
            var unitIdPopup = root.CreatePopup(InstancesEditorState.GetBossesCollection);

            unitIdPopup.Label = "Boss Unit";
            unitIdPopup.Value = unitId;
        }
    }
}