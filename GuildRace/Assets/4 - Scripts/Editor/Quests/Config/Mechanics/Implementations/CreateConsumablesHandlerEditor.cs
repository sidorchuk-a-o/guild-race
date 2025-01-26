using AD.ToolsCollection;
using Game.Inventory;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Game.Quests
{
    [QuestsEditor(typeof(CreateConsumablesHandler))]
    public class CreateConsumablesHandlerEditor : QuestMechanicHandlerEditor
    {
        public override void CreateQuestParamsEditor(VisualElement root, List<string> questParams)
        {
            var itemId = questParams[0].IntParse();
            var itemIdPopup = root.CreatePopup(InventoryEditorState.GetAllItemsCollection);

            itemIdPopup.label = "Consumable";
            itemIdPopup.value = itemId;
        }
    }
}