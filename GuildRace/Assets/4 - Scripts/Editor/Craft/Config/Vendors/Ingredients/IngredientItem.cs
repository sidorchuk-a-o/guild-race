using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Craft
{
    /// <summary>
    /// Item: <see cref="IngredientData"/>
    /// </summary>
    public class IngredientItem : ListItemElement
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
            countField.labelOn = false;

            reagentPopup = root.CreatePopup(CraftEditorState.GetReagentsCollection);
            reagentPopup.FlexGrow(1);
            reagentPopup.labelOn = false;
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            countField.BindProperty("count", data);
            reagentPopup.BindProperty("reagentId", data);
        }
    }
}