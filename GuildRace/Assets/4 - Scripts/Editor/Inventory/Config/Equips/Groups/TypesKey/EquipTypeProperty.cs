using AD.ToolsCollection;
using UnityEditor;

namespace Game.Inventory
{
    [CustomPropertyDrawer(typeof(EquipType))]
    public class EquipTypeProperty : KeyPropertyDrawer<EquipType, int>
    {
    }
}