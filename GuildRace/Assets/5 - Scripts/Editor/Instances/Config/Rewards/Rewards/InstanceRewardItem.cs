using System.Collections.Generic;
using System.Linq;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Instances
{
    public class InstanceRewardItem : ListItemElement<InstanceRewardData>
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
            idLabel.LabelOn = false;

            titleLabel = root.CreateElement<LabelElement>();
            titleLabel.Width(200).FontSize(16).Height(100, LengthUnit.Percent);
            titleLabel.LabelOn = false;

            mechanicIdPopup = root.CreatePopup(InstancesEditorState.CreateRewardMechanicsViewCollection);
            mechanicIdPopup.Width(120).FontSize(16);
            mechanicIdPopup.LabelOn = false;

            mechanicParamsContainer = root.CreateContainer();
            mechanicParamsContainer.FlexGrow(1).FontSize(16).PaddingLeft(5);
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            idLabel.BindProperty("id", data);
            titleLabel.BindProperty("title", data);
            mechanicIdPopup.BindProperty("mechanicId", data);

            TitleLabel.Display(false);

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

            var config = InstancesEditorState.Config;
            var rewardHandlers = config.RewardsParams.RewardHandlers;
            var mechanicData = rewardHandlers.FirstOrDefault(x => x.Id == mechanicId);

            if (mechanicData == null)
            {
                return;
            }

            var factory = DataEditorsFactory.Instance;
            var mechanicEditor = (RewardHandlerEditor)factory.CreateEditor(mechanicData.GetType());

            mechanicParamsContainer.Clear();

            mechanicEditor.CreateParamsEditor(mechanicParamsContainer, mechanicParams);
        }
    }
}