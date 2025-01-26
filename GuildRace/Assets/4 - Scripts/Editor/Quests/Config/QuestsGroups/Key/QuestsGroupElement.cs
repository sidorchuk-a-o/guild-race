using AD.ToolsCollection;
using System;

namespace Game.Quests
{
    [KeyElement(typeof(QuestsGroup))]
    public class QuestsGroupElement : KeyElement<int>
    {
        protected override Func<Collection<int>> GetCollection
        {
            get => QuestsEditorState.CreateQuestsGroupViewCollection;
        }

        protected override void CreateElementGUI(Element root)
        {
            base.CreateElementGUI(root);

            formatSelectedValueOn = false;
        }
    }
}