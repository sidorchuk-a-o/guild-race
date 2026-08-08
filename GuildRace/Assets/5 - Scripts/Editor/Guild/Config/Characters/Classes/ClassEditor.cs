using AD.Localization;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Guild
{
    [DataEditor(typeof(ClassData))]
    public class ClassEditor : DataEditor
    {
        private LocalizeKeyElement nameKeyField;
        private PropertyElement armorTypeField;
        private PropertyElement weaponTypeField;
        private SpecializationsList specsList;

        protected override void CreateContentGUI(VisualElement root)
        {
            base.CreateContentGUI(root);

            root.CreateHeader("Common");

            nameKeyField = (LocalizeKeyElement)root.CreateKeyPopup<LocalizeKey, string>();
            nameKeyField.PreviewOn = true;

            root.CreateHeader("Equips");

            armorTypeField = root.CreateProperty();
            weaponTypeField = root.CreateProperty();

            root.CreateHeader("Specs");

            specsList = root.CreateElement<SpecializationsList>();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            nameKeyField.BindProperty("nameKey", data);
            armorTypeField.BindProperty("armorType", data);
            weaponTypeField.BindProperty("weaponType", data);
            specsList.BindProperty("specs", data);
        }
    }
}