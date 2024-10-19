using AD.ToolsCollection;
using System;

namespace Game.Items
{
    [Serializable]
    public class ItemSourceKey : Key
    {
        public ItemSourceKey()
        {
        }

        public ItemSourceKey(string key) : base(key)
        {
        }

        public static implicit operator string(ItemSourceKey key) => key?.value;
        public static implicit operator ItemSourceKey(string key) => new(key);
    }
}