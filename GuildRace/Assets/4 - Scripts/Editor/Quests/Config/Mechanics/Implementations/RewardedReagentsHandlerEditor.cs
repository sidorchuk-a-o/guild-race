using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Game.Quests
{
    [QuestsEditor(typeof(RewardedReagentsHandler))]
    public class RewardedReagentsHandlerEditor : QuestMechanicHandlerEditor
    {
        public override void CreateQuestParamsEditor(VisualElement root, List<string> questParams)
        {
        }
    }
}