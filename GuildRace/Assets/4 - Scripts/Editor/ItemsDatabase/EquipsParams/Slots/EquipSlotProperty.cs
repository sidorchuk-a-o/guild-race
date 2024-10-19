using AD.ToolsCollection;
using UnityEditor;

namespace Game.Items
{
    [CustomPropertyDrawer(typeof(EquipSlot))]
    public class EquipSlotProperty : KeyPropertyDrawer<EquipSlot>
    {
    }
}