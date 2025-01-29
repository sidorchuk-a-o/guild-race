using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Game.Quests
{
    [QuestsEditor(typeof(KillingBossHandler))]
    public class KillingBossHandlerEditor : QuestMechanicHandlerEditor
    {
        public override void CreateQuestParamsEditor(VisualElement root, List<string> questParams)
        {
        }
    }
}