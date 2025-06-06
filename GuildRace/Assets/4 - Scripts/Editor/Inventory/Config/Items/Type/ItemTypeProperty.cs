using AD.ToolsCollection;
using UnityEditor;

namespace Game.Inventory
{
    [CustomPropertyDrawer(typeof(ItemType))]
    public class ItemTypeProperty : KeyPropertyDrawer<ItemType, int>
    {
    }
}