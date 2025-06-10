using System;
using AD.ToolsCollection;

namespace Game.Inventory
{
    [Serializable]
    public class ItemType : Key<int>
    {
        public ItemType()
        {
        }

        public ItemType(int key) : base(key)
        {
        }

        public static implicit operator int(ItemType key) => key?.value ?? -1;
        public static implicit operator ItemType(int key) => new(key);
    }
}