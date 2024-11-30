using AD.ToolsCollection;
using UnityEditor;

namespace Game.Inventory
{
    [CustomPropertyDrawer(typeof(ItemsGridCellType))]
    public class ItemsGridCellTypeProperty : KeyPropertyDrawer<ItemsGridCellType, int>
    {
    }
}