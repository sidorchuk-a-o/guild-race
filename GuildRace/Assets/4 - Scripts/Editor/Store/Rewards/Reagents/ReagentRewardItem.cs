using AD.ToolsCollection;
using UnityEngine.UIElements;
using Game.Craft;

namespace Game.Store
{
    /// <summary>
    /// Item: <see cref="ReagentRewardData"/>
    /// </summary>
    public class ReagentRewardItem : ListItemElement
    {
        private PropertyElement amountField;
        private PopupElement<int> reagentPopup;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            root.ConvertToRow();

            amountField = root.CreateProperty();
            amountField.Width(80);
            amountField.labelOn = false;

            reagentPopup = root.CreatePopup(CraftEditorState.GetReagentsCollection);
            reagentPopup.FlexGrow(1);
            reagentPopup.labelOn = false;
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            amountField.BindProperty("amount", data);
            reagentPopup.BindProperty("reagentId", data);
        }
    }
}