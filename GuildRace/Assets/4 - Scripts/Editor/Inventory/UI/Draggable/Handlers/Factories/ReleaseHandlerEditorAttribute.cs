using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    public class ReleaseHandlerEditorAttribute : EditorAttribute
    {
        public ReleaseHandlerEditorAttribute(Type dataType) : base(dataType)
        {
        }
    }
}