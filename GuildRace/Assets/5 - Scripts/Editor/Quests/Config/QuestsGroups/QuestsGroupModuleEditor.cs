using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace AD.Game.Quests
{
    /// <summary>
    ///     Editor: <see cref="QuestsGroupModule" />
    /// </summary>
    public abstract class QuestsGroupModuleEditor : DataEditor
    {
        private PropertyElement nameKeyPopup;
        private PropertyElement maxQuestsCountField;

        protected override void CreateContentGUI(VisualElement root)
        {
            base.CreateContentGUI(root);

            nameKeyPopup = root.CreateProperty();
            maxQuestsCountField = root.CreateProperty();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            nameKeyPopup.BindProperty("nameKey", data);
            maxQuestsCountField.BindProperty("maxQuestsCount", data);
        }
    }
}