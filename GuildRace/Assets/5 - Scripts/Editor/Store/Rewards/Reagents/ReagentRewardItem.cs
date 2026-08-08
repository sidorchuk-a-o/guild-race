using AD.Game.Craft;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Store
{
    public class ReagentRewardItem : ListItemElement<ReagentRewardData>
    {
        private PropertyElement amountField;
        private PopupElement<int> reagentPopup;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            root.ConvertToRow();

            amountField = root.CreateProperty();
            amountField.Width(80);
            amountField.LabelOn = false;

            reagentPopup = root.CreatePopup(CraftEditorState.GetReagentsCollection);
            reagentPopup.FlexGrow(1);
            reagentPopup.LabelOn = false;
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            amountField.BindProperty("amount", data);
            reagentPopup.BindProperty("reagentId", data);
        }
    }
}