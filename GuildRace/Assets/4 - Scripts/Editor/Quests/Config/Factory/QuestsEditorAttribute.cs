using AD.ToolsCollection;
using System;

namespace Game.Quests
{
    public class QuestsEditorAttribute : EditorAttribute
    {
        public QuestsEditorAttribute(Type dataType) : base(dataType)
        {
        }
    }
}