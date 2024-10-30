using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    [KeyElement(typeof(OptionKey))]
    public class OptionKeyElement : KeyElement
    {
        protected override Func<Collection<string>> GetCollection
        {
            get => InventoryEditorState.CreateOptionsViewCollection;
        }
    }
}