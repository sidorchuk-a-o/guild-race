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

        protected override void CreateEditorGUI(VisualElement root)
        {
            base.CreateEditorGUI(root);

            contentContainer.CreateHeader("Common");

            nameKeyField = contentContainer.CreateKey<LocalizeKey>() as LocalizeKeyElement;
            nameKeyField.previewOn = true;

            contentContainer.CreateHeader("Equips");

            armorTypeField = contentContainer.CreateProperty();
            weaponTypeField = contentContainer.CreateProperty();

            contentContainer.CreateHeader("Specs");

            specsList = contentContainer.CreateElement<SpecializationsList>();
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