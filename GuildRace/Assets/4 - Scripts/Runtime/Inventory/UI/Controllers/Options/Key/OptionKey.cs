using AD.ToolsCollection;
using System;

namespace Game.Inventory
{
    [Serializable]
    public class OptionKey : Key
    {
        public OptionKey()
        {
        }

        public OptionKey(string key) : base(key)
        {
        }

        public static implicit operator string(OptionKey key) => key?.value;
        public static implicit operator OptionKey(string key) => new(key);
    }
}