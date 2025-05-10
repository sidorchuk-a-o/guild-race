using System.Collections.Generic;
using AD.ToolsCollection;
using Game.Inventory;
using UnityEngine.UIElements;

namespace Game.Instances
{
    [InstancesEditor(typeof(ReagentRewardHandler))]
    public class ReagentRewardHandlerEditor : RewardHandlerEditor
    {
        public override void CreateParamsEditor(VisualElement root, List<string> rewardParams)
        {
            root.ConvertToRow();

            var equipId = rewardParams[0].IntParse();
            var countValue = rewardParams[1];

            var countLabel = root.CreateElement<LabelElement>();
            var equipElement = root.CreatePopup(InventoryEditorState.GetAllItemsCollection());

            countLabel.text = countValue;
            equipElement.value = equipId;

            equipElement.ReadOnly();
        }
    }
}