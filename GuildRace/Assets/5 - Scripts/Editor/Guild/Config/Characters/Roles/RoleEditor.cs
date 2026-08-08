using AD.Localization;
using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Guild
{
    [DataEditor(typeof(RoleData))]
    public class RoleEditor : DataEditor
    {
        private LocalizeKeyElement nameKeyField;

        protected override void CreateContentGUI(VisualElement root)
        {
            base.CreateContentGUI(root);

            nameKeyField = (LocalizeKeyElement)root.CreateKeyPopup<LocalizeKey, string>();
            nameKeyField.PreviewOn = true;
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            nameKeyField.BindProperty("nameKey", data);
        }
    }
}