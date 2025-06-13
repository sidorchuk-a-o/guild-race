using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Instances
{
    /// <summary>
    /// Item: <see cref="RewardMechanicUIParams"/>
    /// </summary>
    public class RewardMechanicUIParamsItem : ListItemElement
    {
        private PopupElement<int> mechanicField;

        protected override IEditorsFactory EditorsFactory => InstancesEditorState.EditorsFactory;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            mechanicField = root.CreatePopup(InstancesEditorState.CreateRewardMechanicsViewCollection);
            mechanicField.FlexGrow(1).ReadOnly();
            mechanicField.labelOn = false;
        }

        public override void BindData(SerializedData data)
        {
            foldoutOn = true;

            base.BindData(data);

            mechanicField.BindProperty("mechanicId", data);
        }
    }
}