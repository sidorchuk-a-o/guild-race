using AD.Services.Localization;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Guild
{
    [GuildEditor(typeof(RoleData))]
    public class RoleEditor : EntityEditor
    {
        private LocalizeKeyElement nameKeyField;

        protected override void CreateSimpleContentGUI(VisualElement root)
        {
            base.CreateSimpleContentGUI(root);

            nameKeyField = root.CreateKey<LocalizeKey, string>() as LocalizeKeyElement;
            nameKeyField.previewOn = true;
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            nameKeyField.BindProperty("nameKey", data);
        }
    }
}