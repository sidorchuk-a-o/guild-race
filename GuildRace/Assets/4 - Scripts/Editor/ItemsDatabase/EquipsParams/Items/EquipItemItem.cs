using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Items
{
    /// <summary>
    /// Item: <see cref="EquipItemData"/>
    /// </summary>
    public class EquipItemItem : ListItemElement
    {
        private SpriteField iconField;
        private PropertyElement levelField;
        private PropertyElement powerField;
        private KeyElement typeField;
        private KeyElement slotField;
        private KeyElement rarityField;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            root.ConvertToRow();
            root.AlignItems(Align.Center);

            iconField = root.CreateElement<SpriteField>();
            iconField.labelOn = false;
            iconField.nameOn = false;

            levelField = root.CreateProperty();
            levelField.Width(75);
            levelField.FontSize(18);
            levelField.labelOn = false;

            powerField = root.CreateProperty();
            powerField.Width(125);
            powerField.FontSize(18);
            powerField.labelOn = false;

            typeField = root.CreateKey<EquipType>();
            typeField.Width(200).Height(66, LengthUnit.Percent);
            typeField.FontSize(16);
            typeField.labelOn = false;
            typeField.removeOn = false;
            typeField.filterOn = false;
            typeField.updateOn = false;

            slotField = root.CreateKey<EquipSlot>();
            slotField.Width(120).Height(66, LengthUnit.Percent);
            slotField.FontSize(16);
            slotField.labelOn = false;
            slotField.removeOn = false;
            slotField.filterOn = false;
            slotField.updateOn = false;

            rarityField = root.CreateKey<Rarity>();
            rarityField.Width(120).Height(66, LengthUnit.Percent);
            rarityField.FontSize(16);
            rarityField.labelOn = false;
            rarityField.removeOn = false;
            rarityField.filterOn = false;
            rarityField.updateOn = false;
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            iconField.BindProperty("iconRef", data);
            levelField.BindProperty("level", data);
            powerField.BindProperty("power", data);
            rarityField.BindProperty("rarity", data);
            typeField.BindProperty("type", data);
            slotField.BindProperty("slot", data);
        }
    }
}