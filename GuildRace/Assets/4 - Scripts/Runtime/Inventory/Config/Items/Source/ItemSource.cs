using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    [Serializable]
    public class ItemSource : Key<int>
    {
        public ItemSource()
        {
        }

        public ItemSource(int key) : base(key)
        {
        }

        public static implicit operator int(ItemSource key) => key?.value ?? -1;
        public static implicit operator ItemSource(int key) => new(key);
    }
}