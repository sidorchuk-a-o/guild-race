using System;
using AD.ToolsCollection;
using UnityEditor;

namespace AD.Game.Inventory
{
    [CustomPropertyDrawer(typeof(OptionKey))]
    public class OptionKeyProperty : KeyPropertyDrawer<OptionKey, string>
    {
    }

    [KeyEditor(typeof(OptionKey))]
    public class OptionKeyElement : KeyElement<string>
    {
        protected override Func<Collection<string>> GetCollection
        {
            get => InventoryEditorState.CreateOptionsViewCollection;
        }
    }
}