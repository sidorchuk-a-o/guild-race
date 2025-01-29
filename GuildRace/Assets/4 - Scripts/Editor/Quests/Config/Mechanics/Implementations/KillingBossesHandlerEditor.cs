using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Game.Quests
{
    [QuestsEditor(typeof(KillingBossesHandler))]
    public class KillingBossesHandlerEditor : QuestMechanicHandlerEditor
    {
        public override void CreateQuestParamsEditor(VisualElement root, List<string> questParams)
        {
        }
    }
}