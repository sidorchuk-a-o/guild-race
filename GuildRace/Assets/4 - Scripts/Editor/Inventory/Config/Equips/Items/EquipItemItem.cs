using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    /// <summary>
    /// Item: <see cref="EquipItemData"/>
    /// </summary>
    public class EquipItemItem : ListItemElement
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
            iconField.labelOn = false;
            iconField.nameOn = false;

            idLabel = root.CreateProperty();
            idLabel.Width(70).FontSize(16).ReadOnly();
            idLabel.labelOn = false;

            titleLabel = root.CreateElement<LabelElement>();
            titleLabel.FlexGrow(1).FontSize(16).Height(100, LengthUnit.Percent);
            titleLabel.labelOn = false;

            levelField = root.CreateProperty();
            levelField.Width(60).FontSize(16);
            levelField.labelOn = false;

            powerField = root.CreateProperty();
            powerField.Width(56).FontSize(14).MarginRight(-9);
            powerField.labelOn = false;

            healthField = root.CreateProperty();
            healthField.Width(56).FontSize(14);
            healthField.labelOn = false;

            typeField = root.CreateKey<EquipType, int>();
            typeField.Width(200).FontSize(16);
            typeField.labelOn = false;
            typeField.removeOn = false;
            typeField.filterOn = false;
            typeField.updateOn = false;

            itemSlotField = root.CreateKey<ItemSlot, int>();
            itemSlotField.Width(175).FontSize(16);
            itemSlotField.labelOn = false;
            itemSlotField.removeOn = false;
            itemSlotField.filterOn = false;
            itemSlotField.updateOn = false;

            rarityField = root.CreateKey<Rarity, int>();
            rarityField.Width(120).FontSize(16);
            rarityField.labelOn = false;
            rarityField.removeOn = false;
            rarityField.filterOn = false;
            rarityField.updateOn = false;
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
        }
    }
}