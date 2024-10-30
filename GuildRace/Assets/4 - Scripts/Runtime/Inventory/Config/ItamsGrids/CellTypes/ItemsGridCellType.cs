using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    [Serializable]
    public class ItemsGridCellType : Key
    {
        public ItemsGridCellType()
        {
        }

        public ItemsGridCellType(string key) : base(key)
        {
        }

        public static implicit operator string(ItemsGridCellType key) => key?.value;
        public static implicit operator ItemsGridCellType(string key) => new(key);
    }
}