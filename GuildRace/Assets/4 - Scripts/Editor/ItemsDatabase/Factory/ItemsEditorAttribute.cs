using AD.ToolsCollection;
using System;

namespace Game.Items
{
    public class ItemsEditorAttribute : EditorAttribute
    {
        public ItemsEditorAttribute(Type dataType) : base(dataType)
        {
        }
    }
}