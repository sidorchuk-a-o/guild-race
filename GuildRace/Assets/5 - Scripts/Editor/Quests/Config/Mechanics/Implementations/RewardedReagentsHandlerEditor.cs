using System.Collections.Generic;
using AD.Game.Inventory;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Quests
{
    [DataEditor(typeof(RewardedReagentsHandler))]
    public class RewardedReagentsHandlerEditor : QuestMechanicHandlerEditor
    {
        public override void CreateQuestParamsEditor(VisualElement root, List<string> questParams)
        {
            var itemId = questParams[0].IntParse();
            var itemIdPopup = root.CreatePopup(InventoryEditorState.GetAllItemsCollection);

            itemIdPopup.Label = "Reagent";
            itemIdPopup.Value = itemId;
        }
    }
}