using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Instances
{
    public class RewardMechanicUIParamsItem : ListItemElement<RewardMechanicUIParams>
    {
        private PopupElement<int> mechanicField;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            mechanicField = root.CreatePopup(InstancesEditorState.CreateRewardMechanicsViewCollection);
            mechanicField.FlexGrow(1).ReadOnly();
            mechanicField.LabelOn = false;
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            FoldoutOn = true;

            mechanicField.BindProperty("mechanicId", data);
        }
    }
}