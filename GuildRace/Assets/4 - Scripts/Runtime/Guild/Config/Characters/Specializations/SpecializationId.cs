using AD.ToolsCollection;
using System;

namespace Game.Guild
{
    [Serializable]
    public class SpecializationId : Key<int>
    {
        public SpecializationId()
        {
        }

        public SpecializationId(int key) : base(key)
        {
        }

        public static implicit operator int(SpecializationId key) => key?.value ?? -1;
        public static implicit operator SpecializationId(int key) => new(key);
    }
}