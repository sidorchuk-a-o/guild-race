using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    [Serializable]
    public class ItemSlot : Key<int>
    {
        public ItemSlot()
        {
        }

        public ItemSlot(int key) : base(key)
        {
        }

        public static implicit operator int(ItemSlot key) => key?.value ?? -1;
        public static implicit operator ItemSlot(int key) => new(key);
    }
}