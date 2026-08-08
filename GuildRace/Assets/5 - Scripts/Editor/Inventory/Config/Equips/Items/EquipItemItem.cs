using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Inventory
{
    public class EquipItemItem : ListItemElement<EquipItemData>
    {
        private SpriteField iconField;
        private PropertyElement idLabel;
        private LabelElement titleLabel;
        private PropertyElement levelField;
        private PropertyElement powerField;
        private PropertyElement healthField;
        private KeyElement<int> typeField;
        private KeyElement<int> itemSlotField;
        private KeyElement<int> rarityField;

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

            levelField = root.CreateProperty();
            levelField.Width(60).FontSize(16);
            levelField.LabelOn = false;

            powerField = root.CreateProperty();
            powerField.Width(56).FontSize(14).MarginRight(-9);
            powerField.LabelOn = false;

            healthField = root.CreateProperty();
            healthField.Width(56).FontSize(14);
            healthField.LabelOn = false;

            typeField = root.CreateKeyPopup<EquipType, int>();
            typeField.Width(200).FontSize(16);
            typeField.LabelOn = false;
            typeField.RemoveOn = false;
            typeField.FilterOn = false;
            typeField.UpdateOn = false;

            itemSlotField = root.CreateKeyPopup<ItemSlot, int>();
            itemSlotField.Width(175).FontSize(16);
            itemSlotField.LabelOn = false;
            itemSlotField.RemoveOn = false;
            itemSlotField.FilterOn = false;
            itemSlotField.UpdateOn = false;

            rarityField = root.CreateKeyPopup<Rarity, int>();
            rarityField.Width(120).FontSize(16);
            rarityField.LabelOn = false;
            rarityField.RemoveOn = false;
            rarityField.FilterOn = false;
            rarityField.UpdateOn = false;
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            var characterData = data.GetProperty("characterParams");

            idLabel.BindProperty("id", data);
            titleLabel.BindProperty("title", data);
            iconField.BindProperty("iconRef", data);
            levelField.BindProperty("level", data);
            powerField.BindProperty("power", characterData);
            healthField.BindProperty("health", characterData);
            typeField.BindProperty("type", data);
            itemSlotField.BindProperty("slot", data);
            rarityField.BindProperty("rarity", data);

            TitleLabel.Display(false);
        }
    }
}