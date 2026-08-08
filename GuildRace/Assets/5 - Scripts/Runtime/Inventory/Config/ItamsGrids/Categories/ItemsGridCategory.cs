using System;
using AD.ToolsCollection;

namespace AD.Game.Inventory
{
    [Serializable]
    public class ItemsGridCategory : Key<int>
    {
        public ItemsGridCategory()
        {
        }

        public ItemsGridCategory(int key) : base(key)
        {
        }

        public static implicit operator int(ItemsGridCategory key) => key?.value ?? -1;
        public static implicit operator ItemsGridCategory(int key) => new(key);
    }
}