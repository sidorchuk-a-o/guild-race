using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    [Serializable]
    public class ItemSource : Key
    {
        public ItemSource()
        {
        }

        public ItemSource(string key) : base(key)
        {
        }

        public static implicit operator string(ItemSource key) => key?.value;
        public static implicit operator ItemSource(string key) => new(key);
    }
}