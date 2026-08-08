using System;
using AD.ToolsCollection;
using UnityEditor;

namespace AD.Game.Inventory
{
    [CustomPropertyDrawer(typeof(EquipGroup))]
    public class EquipGroupProperty : KeyPropertyDrawer<EquipGroup, int>
    {
    }

    [KeyEditor(typeof(EquipGroup))]
    public class EquipGroupElement : KeyElement<int>
    {
        protected override Func<Collection<int>> GetCollection
        {
            get => InventoryEditorState.GetEquipGroupsCollection;
        }
    }
}