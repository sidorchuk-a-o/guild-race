using AD.Services.Localization;
using AD.ToolsCollection;

namespace Game.Inventory
{
    [InventoryEditor(typeof(EquipGroupData))]
    public class EquipGroupFoldoutElement : Element
    {
        private KeyElement<string> nameKeyField;
        private EquipTypesList typesList;

        protected override void CreateElementGUI(Element root)
        {
            nameKeyField = root.CreateKey<LocalizeKey, string>();
            nameKeyField.labelOn = false;

            root.CreateSpace();

            typesList = root.CreateElement<EquipTypesList>();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            nameKeyField.BindProperty("nameKey", data);
            typesList.BindProperty("types", data);
        }
    }
}