using System.Collections.Generic;
using AD.Game.Inventory;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Quests
{
    [DataEditor(typeof(CreateConsumablesHandler))]
    public class CreateConsumablesHandlerEditor : QuestMechanicHandlerEditor
    {
        public override void CreateQuestParamsEditor(VisualElement root, List<string> questParams)
        {
            var itemId = questParams[0].IntParse();
            var itemIdPopup = root.CreatePopup(InventoryEditorState.GetAllItemsCollection);

            itemIdPopup.Label = "Consumable";
            itemIdPopup.Value = itemId;
        }
    }
}