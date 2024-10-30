using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    [Serializable]
    public class ItemSlot : Key
    {
        public ItemSlot()
        {
        }

        public ItemSlot(string key) : base(key)
        {
        }

        public static implicit operator string(ItemSlot key) => key?.value;
        public static implicit operator ItemSlot(string key) => new(key);
    }
}