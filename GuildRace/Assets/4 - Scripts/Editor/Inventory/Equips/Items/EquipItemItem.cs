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
        private PropertyElement levelField;
        private PropertyElement powerField;
        private KeyElement typeField;
        private KeyElement itemSlotField;
        private KeyElement rarityField;

        protected override void CreateItemContentGUI(VisualElement root)
        {
            base.CreateItemContentGUI(root);

            root.ConvertToRow();
            root.AlignItems(Align.Center);

            iconField = root.CreateElement<SpriteField>();
            iconField.SetSize(35);
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
            typeField.Width(200);
            typeField.FontSize(16);
            typeField.labelOn = false;
            typeField.removeOn = false;
            typeField.filterOn = false;
            typeField.updateOn = false;

            itemSlotField = root.CreateKey<ItemSlot>();
            itemSlotField.Width(175);
            itemSlotField.FontSize(16);
            itemSlotField.labelOn = false;
            itemSlotField.removeOn = false;
            itemSlotField.filterOn = false;
            itemSlotField.updateOn = false;

            rarityField = root.CreateKey<Rarity>();
            rarityField.Width(120);
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
            typeField.BindProperty("type", data);
            itemSlotField.BindProperty("slot", data);
            rarityField.BindProperty("rarity", data);
        }
    }
}