using System.Collections.Generic;
using AD.ToolsCollection;
using Game.Inventory;
using UnityEngine.UIElements;

namespace Game.Quests
{
    [QuestsEditor(typeof(RewardedReagentsHandler))]
    public class RewardedReagentsHandlerEditor : QuestMechanicHandlerEditor
    {
        public override void CreateQuestParamsEditor(VisualElement root, List<string> questParams)
        {
            var itemId = questParams[0].IntParse();
            var itemIdPopup = root.CreatePopup(InventoryEditorState.GetAllItemsCollection);

            itemIdPopup.label = "Reagent";
            itemIdPopup.value = itemId;
        }
    }
}