using System.Collections.Generic;
using System.Linq;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Instances
{
    /// <summary>
    /// Item: <see cref="InstanceRewardData"/>
    /// </summary>
    public class InstanceRewardItem : ListItemElement
    {
        private PropertyElement idLabel;
        private LabelElement titleLabel;
        private PopupElement<int> mechanicIdPopup;
        private VisualElement mechanicParamsContainer;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            idLabel = root.CreateProperty();
            idLabel.Width(70).FontSize(16).ReadOnly();
            idLabel.labelOn = false;

            titleLabel = root.CreateElement<LabelElement>();
            titleLabel.Width(200).FontSize(16).Height(100, LengthUnit.Percent);
            titleLabel.labelOn = false;

            mechanicIdPopup = root.CreatePopup(InstancesEditorState.CreateRewardMechanicsViewCollection, false);
            mechanicIdPopup.Width(120).FontSize(16);
            mechanicIdPopup.labelOn = false;

            mechanicParamsContainer = root.CreateContainer();
            mechanicParamsContainer.FlexGrow(1).FontSize(16).PaddingLeft(5);
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            idLabel.BindProperty("id", data);
            titleLabel.BindProperty("title", data);
            mechanicIdPopup.BindProperty("mechanicId", data);

            BindMechanicParams(data);
        }

        private void BindMechanicParams(SerializedData data)
        {
            var mechanicId = data
                .GetProperty("mechanicId")
                .GetValue<int>();

            var mechanicParams = data
                .GetProperty("mechanicParams")
                .GetValue<List<string>>();

            var mechanicData = InstancesEditorState.Config.RewardsParams.RewardHandlers.FirstOrDefault(x => x.Id == mechanicId);

            if (mechanicData == null)
            {
                return;
            }

            var mechanicEditor = InstancesEditorState.EditorsFactory.CreateEditor(mechanicData.GetType()) as RewardHandlerEditor;

            mechanicParamsContainer.Clear();
            mechanicEditor.CreateParamsEditor(mechanicParamsContainer, mechanicParams);
        }
    }
}