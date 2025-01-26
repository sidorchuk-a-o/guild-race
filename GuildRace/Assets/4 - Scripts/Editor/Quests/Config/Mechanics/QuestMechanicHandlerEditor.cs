using AD.ToolsCollection;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Game.Quests
{
    /// <summary>
    /// Editor: <see cref="QuestMechanicHandler"/>
    /// </summary>
    public abstract class QuestMechanicHandlerEditor : EntityEditor
    {
        private PropertyElement nameKeyPopup;
        private PropertyElement descKeyPopup;

        protected override void CreateSimpleContentGUI(VisualElement root)
        {
            base.CreateSimpleContentGUI(root);

            nameKeyPopup = root.CreateProperty();
            descKeyPopup = root.CreateProperty();
        }

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            nameKeyPopup.BindProperty("nameKey", data);
            descKeyPopup.BindProperty("descKey", data);
        }

        public abstract void CreateQuestParamsEditor(VisualElement root, List<string> questParams);
    }
}