using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Craft
{
    public class IngredientItem : ListItemElement<IngredientData>
    {
        private PropertyElement countField;
        private PopupElement<int> reagentPopup;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            root.ConvertToRow();
            root.AlignItems(Align.Center);

            countField = root.CreateProperty();
            countField.Width(60);
            countField.LabelOn = false;

            reagentPopup = root.CreatePopup(CraftEditorState.GetReagentsCollection);
            reagentPopup.FlexGrow(1);
            reagentPopup.LabelOn = false;
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            countField.BindProperty("count", data);
            reagentPopup.BindProperty("reagentId", data);
        }
    }
}