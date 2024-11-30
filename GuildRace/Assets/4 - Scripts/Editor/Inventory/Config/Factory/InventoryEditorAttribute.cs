using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    public class InventoryEditorAttribute : EditorAttribute
    {
        public InventoryEditorAttribute(Type dataType) : base(dataType)
        {
        }
    }
}