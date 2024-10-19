using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;

namespace Game.Items
{
    [CreateWizard(typeof(EquipItemData))]
    public class EquipItemCreateWizard : CreateWizard
    {
        [SerializeField] private AssetReference iconRef;

        private IntegerField levelField;
        private IntegerField powerField;
        private PopupElement<string> rarityField;
        private PopupElement<string> typeField;
        private PopupElement<string> slotField;
        private SpriteField iconRefField;

        private SerializedData iconData;

        protected override void CreateWizardGUI(VisualElement root)
        {
            iconData = new SerializedData(this).GetProperty("iconRef");

            base.CreateWizardGUI(root);

            levelField = root.Create<IntegerField>();
            levelField.label = "Item Level";
            levelField.Focus();

            powerField = root.Create<IntegerField>();
            powerField.label = "Power";

            root.CreateSpace();

            rarityField = root.CreatePopup(ItemsDatabaseEditorState.GetRaritiesCollection);
            rarityField.label = "Rarity";

            typeField = root.CreatePopup(ItemsDatabaseEditorState.GetEquipTypesCollection);
            typeField.label = "Type";

            slotField = root.CreatePopup(ItemsDatabaseEditorState.GetEquipSlotsCollection);
            slotField.label = "Slot";

            root.CreateSpace();

            iconRefField = root.CreateElement<SpriteField>();
            iconRefField.BindData(iconData);
            iconRefField.label = "Icon Ref";
        }

        public override void BindSaveMeta(in SaveMeta saveMeta)
        {
            base.BindSaveMeta(saveMeta);

            ValidateElement(levelField, levelField.value, ValidateLevelField);
            ValidateElement(powerField, powerField.value, ValidatePowerField);
            ValidateElement(rarityField, rarityField.value, ValidateRarityField);
            ValidateElement(typeField, typeField.value, ValidateTypeField);
            ValidateElement(slotField, slotField.value, ValidateSlotField);
            ValidateElement(iconRefField, iconRefField.value, ValidateIconField);
        }

        private string ValidateLevelField(int value)
        {
            const string errorMsg = "Item Level - is empty.";

            var check = levelField.value > 0;

            return check ? string.Empty : errorMsg;
        }

        private string ValidatePowerField(int value)
        {
            const string errorMsg = "Power - is empty.";

            var check = powerField.value > 0;

            return check ? string.Empty : errorMsg;
        }

        private string ValidateRarityField(string value)
        {
            const string errorMsg = "Rarity - is empty.";

            var check = rarityField.value.IsValid();

            return check ? string.Empty : errorMsg;
        }

        private string ValidateTypeField(string value)
        {
            const string errorMsg = "Type - is empty.";

            var check = typeField.value.IsValid();

            return check ? string.Empty : errorMsg;
        }

        private string ValidateSlotField(string value)
        {
            const string errorMsg = "Slot - is empty.";

            var check = slotField.value.IsValid();

            return check ? string.Empty : errorMsg;
        }

        private string ValidateIconField(Object value)
        {
            const string errorMsg = "Icon - is empty.";

            var check = iconRefField.value != null;

            return check ? string.Empty : errorMsg;
        }

        protected override void PreSave()
        {
            base.PreSave();

            newData.SetValue("level", levelField.value);
            newData.SetValue("power", powerField.value);
            newData.SetValue("rarity", new Rarity(rarityField.value));
            newData.SetValue("type", new EquipType(typeField.value));
            newData.SetValue("slot", new EquipSlot(slotField.value));
            newData.SetValue("iconRef", iconRef);
        }
    }
}