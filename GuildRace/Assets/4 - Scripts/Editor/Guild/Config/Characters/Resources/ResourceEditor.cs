using AD.Services.Localization;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Guild
{
    [GuildEditor(typeof(ResourceData))]
    public class ResourceEditor : EntityEditor
    {
        private LocalizeKeyElement nameKeyField;

        protected override void CreateEditorGUI(VisualElement root)
        {
            base.CreateEditorGUI(root);

            nameKeyField = contentContainer.CreateKey<LocalizeKey, string>() as LocalizeKeyElement;
            nameKeyField.previewOn = true;
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            nameKeyField.BindProperty("nameKey", data);
        }
    }
}