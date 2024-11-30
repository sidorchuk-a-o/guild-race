using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    public class ItemSlotVMEditorAttribute : EditorAttribute
    {
        public ItemSlotVMEditorAttribute(Type dataType) : base(dataType)
        {
        }
    }
}