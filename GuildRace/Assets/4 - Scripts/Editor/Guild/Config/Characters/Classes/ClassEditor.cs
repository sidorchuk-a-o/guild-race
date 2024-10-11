using AD.Services.Localization;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Guild
{
    [GuildEditor(typeof(ClassData))]
    public class ClassEditor : EntityEditor
    {
        private LocalizeKeyElement nameKeyField;
        private SpecializationsList specsList;

        protected override void CreateEditorGUI(VisualElement root)
        {
            base.CreateEditorGUI(root);

            contentContainer.CreateHeader("Common");

            nameKeyField = contentContainer.CreateKey<LocalizeKey>() as LocalizeKeyElement;
            nameKeyField.previewOn = true;

            contentContainer.CreateHeader("Specs");

            specsList = contentContainer.CreateElement<SpecializationsList>();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            nameKeyField.BindProperty("nameKey", data);
            specsList.BindProperty("specs", data);
        }
    }
}