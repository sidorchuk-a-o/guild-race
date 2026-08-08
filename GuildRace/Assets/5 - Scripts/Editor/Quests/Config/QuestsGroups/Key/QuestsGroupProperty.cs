using System;
using AD.ToolsCollection;
using UnityEditor;

namespace AD.Game.Quests
{
    [CustomPropertyDrawer(typeof(QuestsGroup))]
    public class QuestsGroupProperty : KeyPropertyDrawer<QuestsGroup, int>
    {
    }

    [KeyEditor(typeof(QuestsGroup))]
    public class QuestsGroupElement : KeyElement<int>
    {
        protected override Func<Collection<int>> GetCollection
        {
            get => QuestsEditorState.CreateQuestsGroupViewCollection;
        }
    }
}