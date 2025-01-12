using AD.ToolsCollection;
using UnityEditor;

namespace Game.Inventory
{
    [CustomPropertyDrawer(typeof(EquipGroup))]
    public class EquipGroupProperty : KeyPropertyDrawer<EquipGroup, int>
    {
    }
}