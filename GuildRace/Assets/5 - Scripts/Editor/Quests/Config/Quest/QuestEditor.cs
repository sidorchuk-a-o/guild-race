using System.Collections.Generic;
using System.Linq;
using AD.Store;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Quests
{
    [DataEditor(typeof(QuestData))]
    public class QuestEditor : DataEditor
    {
        private PropertyElement groupIdPopup;
        private PopupElement<int> mechanicIdPopup;
        private VisualElement mechanicParamsContainer;
        private PropertyElement requiredProgressField;
        private CurrencyAmountElement rewardField;

        protected override void CreateContentGUI(VisualElement root)
        {
            base.CreateContentGUI(root);

            root.CreateHeader("Group");
            groupIdPopup = root.CreateProperty();

            root.CreateHeader("Objectives");
            mechanicIdPopup = root.CreatePopup(QuestsEditorState.CreateQuestMechanicsViewCollection);
            mechanicParamsContainer = root.CreateContainer();

            root.CreateSpace();
            requiredProgressField = root.CreateProperty();

            root.CreateHeader("Reward");
            rewardField = root.CreateElement<CurrencyAmountElement>();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            groupIdPopup.BindProperty("groupId", data);
            mechanicIdPopup.BindProperty("mechanicId", data);
            requiredProgressField.BindProperty("requiredProgress", data);
            rewardField.BindProperty("reward", data);

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

            var config = QuestsEditorState.Config;
            var mechanicHandlers = config.MechanicHandlers;
            var mechanicData = mechanicHandlers.First(x => x.Id == mechanicId);

            var factory = DataEditorsFactory.Instance;
            var mechanicEditor = (QuestMechanicHandlerEditor)factory.CreateEditor(mechanicData.GetType());

            mechanicEditor.CreateQuestParamsEditor(mechanicParamsContainer, mechanicParams);
        }
    }
}