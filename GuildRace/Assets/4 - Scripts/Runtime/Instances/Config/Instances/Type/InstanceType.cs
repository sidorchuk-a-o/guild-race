using AD.ToolsCollection;
using System;

namespace Game.Instances
{
    [Serializable]
    public class InstanceType : Key<int>
    {
        public InstanceType()
        {
        }

        public InstanceType(int key) : base(key)
        {
        }

        public static implicit operator int(InstanceType key) => key?.value ?? -1;
        public static implicit operator InstanceType(int key) => new(key);
    }
}