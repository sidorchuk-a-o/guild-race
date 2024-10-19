using AD.ToolsCollection;
using UnityEditor;

namespace Game.Items
{
    [CustomPropertyDrawer(typeof(EquipType))]
    public class EquipTypeProperty : KeyPropertyDrawer<EquipType>
    {
    }
}