using AD.Services.Localization;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Guild
{
    [GuildEditor(typeof(SpecializationData))]
    public class SpecializationEditor : EntityEditor
    {
        private LocalizeKeyElement nameKeyField;
        private PropertyElement roleIdField;

        protected override void CreateEditorGUI(VisualElement root)
        {
            base.CreateEditorGUI(root);

            contentContainer.CreateHeader("Common");

            nameKeyField = contentContainer.CreateKey<LocalizeKey>() as LocalizeKeyElement;
            nameKeyField.previewOn = true;

            contentContainer.CreateHeader("Role");

            roleIdField = contentContainer.CreateProperty();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            nameKeyField.BindProperty("nameKey", data);
            roleIdField.BindProperty("roleId", data);
        }
    }
}