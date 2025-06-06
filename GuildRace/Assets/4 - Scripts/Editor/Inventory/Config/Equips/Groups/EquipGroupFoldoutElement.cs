using AD.Services.Localization;
using AD.ToolsCollection;
using UnityEngine;

namespace Game.Inventory
{
    [InventoryEditor(typeof(EquipGroupData))]
    public class EquipGroupFoldoutElement : Element
    {
        private AddressableElement<Sprite> iconRefField;
        private LocalizeKeyElement nameKeyField;
        private EquipTypesList typesList;

        protected override void CreateElementGUI(Element root)
        {
            iconRefField = root.CreateAddressable<Sprite>();

            nameKeyField = root.CreateKey<LocalizeKey, string>() as LocalizeKeyElement;
            nameKeyField.previewOn = true;
            nameKeyField.labelOn = false;

            root.CreateSpace();

            typesList = root.CreateElement<EquipTypesList>();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            iconRefField.BindProperty("iconRef", data);
            nameKeyField.BindProperty("nameKey", data);
            typesList.BindProperty("types", data);
        }
    }
}