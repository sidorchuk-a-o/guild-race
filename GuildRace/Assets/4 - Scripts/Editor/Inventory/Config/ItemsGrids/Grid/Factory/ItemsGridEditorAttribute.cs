using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    public class ItemsGridEditorAttribute : EditorAttribute
    {
        public ItemsGridEditorAttribute(Type dataType) : base(dataType)
        {
        }
    }
}