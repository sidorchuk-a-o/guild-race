using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    public class PickupHandlerEditorAttribute : EditorAttribute
    {
        public PickupHandlerEditorAttribute(Type dataType) : base(dataType)
        {
        }
    }
}