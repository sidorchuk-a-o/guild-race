using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    public class OptionHandlerEditorAttribute : EditorAttribute
    {
        public OptionHandlerEditorAttribute(Type dataType) : base(dataType)
        {
        }
    }
}