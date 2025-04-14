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
        private PropertyElement subRoleIdField;
        private UnitParamsElement unitParamsField;

        protected override void CreateSimpleContentGUI(VisualElement root)
        {
            base.CreateSimpleContentGUI(root);

            root.CreateHeader("Common");

            nameKeyField = root.CreateKey<LocalizeKey, string>() as LocalizeKeyElement;
            nameKeyField.previewOn = true;

            root.CreateHeader("Role");

            roleIdField = root.CreateProperty();
            subRoleIdField = root.CreateProperty();

            root.CreateHeader("Params");

            unitParamsField = root.CreateElement<UnitParamsElement>();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            nameKeyField.BindProperty("nameKey", data);
            roleIdField.BindProperty("roleId", data);
            subRoleIdField.BindProperty("subRoleId", data);
            unitParamsField.BindProperty("unitParams", data);
        }
    }
}