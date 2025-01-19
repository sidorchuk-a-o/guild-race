using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Craft
{
    /// <summary>
    /// Item: <see cref="ReagentItemData"/>
    /// </summary>
    public class ReagentItemItem : ListItemElement
    {
        private PropertyElement idLabel;
        private LabelElement titleLabel;
        private SpriteField iconField;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            root.ConvertToRow();
            root.AlignItems(Align.Center);

            iconField = root.CreateElement<SpriteField>();
            iconField.SetSize(35);
            iconField.labelOn = false;
            iconField.nameOn = false;

            idLabel = root.CreateProperty();
            idLabel.Width(70).FontSize(16).ReadOnly();
            idLabel.labelOn = false;

            titleLabel = root.CreateElement<LabelElement>();
            titleLabel.FontSize(16);
            titleLabel.labelOn = false;
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            idLabel.BindProperty("id", data);
            iconField.BindProperty("iconRef", data);
            titleLabel.BindProperty("title", data);
        }
    }
}