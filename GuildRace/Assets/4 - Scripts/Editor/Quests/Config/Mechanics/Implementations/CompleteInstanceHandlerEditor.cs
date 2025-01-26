using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Game.Quests
{
    [QuestsEditor(typeof(CompleteInstanceHandler))]
    public class CompleteInstanceHandlerEditor : QuestMechanicHandlerEditor
    {
        public override void CreateQuestParamsEditor(VisualElement root, List<string> questParams)
        {
        }
    }
}