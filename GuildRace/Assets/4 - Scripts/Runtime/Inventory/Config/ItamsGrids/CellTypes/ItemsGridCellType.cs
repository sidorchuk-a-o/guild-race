using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    [Serializable]
    public class ItemsGridCellType : Key<int>
    {
        public ItemsGridCellType()
        {
        }

        public ItemsGridCellType(int key) : base(key)
        {
        }

        public static implicit operator int(ItemsGridCellType key) => key?.value ?? -1;
        public static implicit operator ItemsGridCellType(int key) => new(key);
    }
}