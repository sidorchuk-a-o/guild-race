using AD.Services.Localization;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Guild
{
    [GuildEditor(typeof(ClassData))]
    public class ClassEditor : EntityEditor
    {
        private LocalizeKeyElement nameKeyField;
        private PropertyElement armorTypeField;
        private PropertyElement weaponTypeField;
        private SpecializationsList specsList;

        protected override void CreateSimpleContentGUI(VisualElement root)
        {
            base.CreateSimpleContentGUI(root);

            root.CreateHeader("Common");

            nameKeyField = root.CreateKey<LocalizeKey, string>() as LocalizeKeyElement;
            nameKeyField.previewOn = true;

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