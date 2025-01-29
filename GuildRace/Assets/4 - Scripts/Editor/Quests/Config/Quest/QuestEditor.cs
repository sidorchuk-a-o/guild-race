using AD.Services.Store;
using AD.ToolsCollection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

namespace Game.Quests
{
    [QuestsEditor(typeof(QuestData))]
    public class QuestEditor : EntityEditor
    {
        private PropertyElement groupIdPopup;
        private PopupElement<int> mechanicIdPopup;
        private VisualElement mechanicParamsContainer;
        private PropertyElement requiredProgressField;
        private CurrencyAmountElement rewardField;

        protected override void CreateSimpleContentGUI(VisualElement root)
        {
            base.CreateSimpleContentGUI(root);

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

            var mechanicData = QuestsEditorState.Config.MechanicHandlers.FirstOrDefault(x => x.Id == mechanicId);
            var mechanicEditor = QuestsEditorState.EditorsFactory.CreateEditor(mechanicData.GetType()) as QuestMechanicHandlerEditor;

            mechanicEditor.CreateQuestParamsEditor(mechanicParamsContainer, mechanicParams);
        }
    }
}