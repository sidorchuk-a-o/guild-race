using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Inventory
{
    /// <summary>
    /// Item: <see cref="EquipItemData"/>
    /// </summary>
    public class EquipItemItem : ListItemElement
    {
        private PropertyElement idLabel;
        private SpriteField iconField;
        private PropertyElement levelField;
        private PropertyElement powerField;
        private PropertyElement healthField;
        private PropertyElement armorField;
        private PropertyElement resourceField;
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

            levelField = root.CreateProperty();
            levelField.Width(60).FontSize(16).ReadOnly();
            levelField.labelOn = false;

            powerField = root.CreateProperty();
            powerField.Width(56).FontSize(14).MarginRight(-9).ReadOnly();
            powerField.labelOn = false;

            healthField = root.CreateProperty();
            healthField.Width(56).FontSize(14).MarginRight(-9).ReadOnly();
            healthField.labelOn = false;

            armorField = root.CreateProperty();
            armorField.Width(56).FontSize(14).MarginRight(-9).ReadOnly();
            armorField.labelOn = false;

            resourceField = root.CreateProperty();
            resourceField.Width(56).FontSize(14).ReadOnly();
            resourceField.labelOn = false;

            typeField = root.CreateKey<EquipType, int>();
            typeField.Width(200).FontSize(16).ReadOnly();
            typeField.labelOn = false;
            typeField.removeOn = false;
            typeField.filterOn = false;
            typeField.updateOn = false;

            itemSlotField = root.CreateKey<ItemSlot, int>();
            itemSlotField.Width(175).FontSize(16).ReadOnly();
            itemSlotField.labelOn = false;
            itemSlotField.removeOn = false;
            itemSlotField.filterOn = false;
            itemSlotField.updateOn = false;

            rarityField = root.CreateKey<Rarity, int>();
            rarityField.Width(120).FontSize(16).ReadOnly();
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
            iconField.BindProperty("iconRef", data);
            levelField.BindProperty("level", data);
            powerField.BindProperty("power", characterData);
            healthField.BindProperty("health", characterData);
            armorField.BindProperty("armor", characterData);
            resourceField.BindProperty("resource", characterData);
            typeField.BindProperty("type", data);
            itemSlotField.BindProperty("slot", data);
            rarityField.BindProperty("rarity", data);
        }
    }
}