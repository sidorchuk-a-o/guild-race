using AD.Localization;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Guild
{
    [DataEditor(typeof(SpecializationData))]
    public class SpecializationEditor : DataEditor
    {
        private LocalizeKeyElement nameKeyField;
        private PropertyElement roleIdField;
        private PropertyElement subRoleIdField;
        private UnitParamsElement unitParamsField;
        private AbilitiesList abilitiesList;

        protected override void CreateContentGUI(VisualElement root)
        {
            base.CreateContentGUI(root);

            root.CreateHeader("Common");

            nameKeyField = (LocalizeKeyElement)root.CreateKeyPopup<LocalizeKey, string>();
            nameKeyField.PreviewOn = true;

            root.CreateHeader("Role");

            roleIdField = root.CreateProperty();
            subRoleIdField = root.CreateProperty();

            root.CreateHeader("Params");

            unitParamsField = root.CreateElement<UnitParamsElement>();

            root.CreateHeader("Abilities");

            abilitiesList = root.CreateElement<AbilitiesList>();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            nameKeyField.BindProperty("nameKey", data);
            roleIdField.BindProperty("roleId", data);
            subRoleIdField.BindProperty("subRoleId", data);
            unitParamsField.BindProperty("unitParams", data);
            abilitiesList.BindProperty("abilities", data);
        }
    }
}