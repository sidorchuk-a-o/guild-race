using AD.ToolsCollection;
using System;

namespace Game.Craft
{
    public class CraftEditorAttribute : EditorAttribute
    {
        public CraftEditorAttribute(Type dataType) : base(dataType)
        {
        }
    }
}