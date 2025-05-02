using AD.ToolsCollection;
using Game.Inventory;
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
        private KeyElement<int> rarityField;
        private PropertyElement stackField;

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
            titleLabel.FlexGrow(1).FontSize(16).Height(100, LengthUnit.Percent);
            titleLabel.labelOn = false;

            rarityField = root.CreateKey<Rarity, int>();
            rarityField.Width(120).FontSize(16);
            rarityField.labelOn = false;
            rarityField.removeOn = false;
            rarityField.filterOn = false;
            rarityField.updateOn = false;

            stackField = root.CreateProperty();
            stackField.Width(70).FontSize(16);
            stackField.labelOn = false;
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            idLabel.BindProperty("id", data);
            iconField.BindProperty("iconRef", data);
            titleLabel.BindProperty("title", data);
            rarityField.BindProperty("rarity", data);
            stackField.BindProperty("size", data.GetProperty("stack"));
        }
    }
}