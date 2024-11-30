using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    public class ItemVMEditorAttribute : EditorAttribute
    {
        public ItemVMEditorAttribute(Type dataType) : base(dataType)
        {
        }
    }
}