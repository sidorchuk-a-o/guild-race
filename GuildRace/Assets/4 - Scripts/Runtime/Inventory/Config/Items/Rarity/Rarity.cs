using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    [Serializable]
    public class Rarity : Key<int>
    {
        public Rarity()
        {
        }

        public Rarity(int key) : base(key)
        {
        }

        public static implicit operator int(Rarity key) => key?.value ?? -1;
        public static implicit operator Rarity(int key) => new(key);
    }
}