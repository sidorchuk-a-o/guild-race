using System;
using AD.ToolsCollection;
using UnityEditor;

namespace AD.Game.Inventory
{
    [CustomPropertyDrawer(typeof(EquipType))]
    public class EquipTypeProperty : KeyPropertyDrawer<EquipType, int>
    {
    }

    [KeyEditor(typeof(EquipType))]
    public class EquipTypeElement : KeyElement<int>
    {
        protected override Func<Collection<int>> GetCollection
        {
            get => InventoryEditorState.GetEquipTypesCollection;
        }
    }
}