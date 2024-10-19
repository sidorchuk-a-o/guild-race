using AD.ToolsCollection;
using System;

namespace Game.Items
{
    [Serializable]
    public class Rarity : Key
    {
        public Rarity()
        {
        }

        public Rarity(string key) : base(key)
        {
        }

        public static implicit operator string(Rarity key) => key?.value;
        public static implicit operator Rarity(string key) => new(key);
    }
}