using AD.ToolsCollection;
using UnityEditor;

namespace Game.Inventory
{
    [CustomPropertyDrawer(typeof(ItemsGridCategory))]
    public class ItemsGridCategoryProperty : KeyPropertyDrawer<ItemsGridCategory, int>
    {
    }
}