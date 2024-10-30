using AD.ToolsCollection;
using UnityEditor;

namespace Game.Inventory
{
    [CustomPropertyDrawer(typeof(ItemSlot))]
    public class ItemSlotKeyProperty : KeyPropertyDrawer<ItemSlot>
    {
    }
}