using System;
using AD.ToolsCollection;

namespace AD.Game.Inventory
{
    [Serializable]
    public class OptionKey : Key<string>
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