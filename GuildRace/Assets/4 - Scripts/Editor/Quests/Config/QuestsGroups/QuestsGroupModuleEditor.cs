using AD.ToolsCollection;
using UnityEngine.UIElements;

namespace Game.Quests
{
    /// <summary>
    /// Editor: <see cref="QuestsGroupModule"/>
    /// </summary>
    public abstract class QuestsGroupModuleEditor : EntityEditor
    {
        private PropertyElement nameKeyPopup;
        private PropertyElement maxQuestsCountField;

        protected override void CreateSimpleContentGUI(VisualElement root)
        {
            base.CreateSimpleContentGUI(root);

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