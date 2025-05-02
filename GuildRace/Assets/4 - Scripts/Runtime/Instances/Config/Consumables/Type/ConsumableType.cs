using System;
using AD.ToolsCollection;

namespace Game.Instances
{
    [Serializable]
    public class ConsumableType : Key<int>
    {
        public ConsumableType()
        {
        }

        public ConsumableType(int key) : base(key)
        {
        }

        public static implicit operator int(ConsumableType key) => key?.value ?? -1;
        public static implicit operator ConsumableType(int key) => new(key);
    }
}