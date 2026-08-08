using AD.Localization;
using AD.ToolsCollection;
using UnityEngine;

namespace AD.Game.Inventory
{
    [DataEditor(typeof(EquipGroupData))]
    public class EquipGroupFoldoutElement : Element
    {
        private AddressableElement<Sprite> iconRefField;
        private LocalizeKeyElement nameKeyField;
        private EquipTypesList typesList;

        protected override void CreateElementGUI(Element root)
        {
            iconRefField = root.CreateAddressable<Sprite>();

            nameKeyField = (LocalizeKeyElement)root.CreateKeyPopup<LocalizeKey, string>();
            nameKeyField.PreviewOn = true;
            nameKeyField.LabelOn = false;

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