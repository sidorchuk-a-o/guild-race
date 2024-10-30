using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    public class ItemSlotEditorAttribute : EditorAttribute
    {
        public ItemSlotEditorAttribute(Type dataType) : base(dataType)
        {
        }
    }
}