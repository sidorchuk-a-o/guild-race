using AD.Game.Inventory;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Craft
{
    public class ReagentItemItem : ListItemElement<ReagentItemData>
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
            iconField.LabelOn = false;
            iconField.NameOn = false;

            idLabel = root.CreateProperty();
            idLabel.Width(70).FontSize(16).ReadOnly();
            idLabel.LabelOn = false;

            titleLabel = root.CreateElement<LabelElement>();
            titleLabel.FlexGrow(1).FontSize(16).Height(100, LengthUnit.Percent);
            titleLabel.LabelOn = false;

            rarityField = root.CreateKeyPopup<Rarity, int>();
            rarityField.Width(120).FontSize(16);
            rarityField.LabelOn = false;
            rarityField.RemoveOn = false;
            rarityField.FilterOn = false;
            rarityField.UpdateOn = false;

            stackField = root.CreateProperty();
            stackField.Width(70).FontSize(16);
            stackField.LabelOn = false;
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            idLabel.BindProperty("id", data);
            iconField.BindProperty("iconRef", data);
            titleLabel.BindProperty("title", data);
            rarityField.BindProperty("rarity", data);
            stackField.BindProperty("size", data.GetProperty("stack"));

            TitleLabel.Display(false);
        }
    }
}