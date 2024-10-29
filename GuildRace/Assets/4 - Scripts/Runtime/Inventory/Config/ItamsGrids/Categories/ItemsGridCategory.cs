using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    [Serializable]
    public class ItemsGridCategory : Key
    {
        public ItemsGridCategory()
        {
        }

        public ItemsGridCategory(string key) : base(key)
        {
        }

        public static implicit operator string(ItemsGridCategory key) => key?.value;
        public static implicit operator ItemsGridCategory(string key) => new(key);
    }
}