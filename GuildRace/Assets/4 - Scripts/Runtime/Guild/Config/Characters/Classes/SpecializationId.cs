using AD.ToolsCollection;
using System;

namespace Game.Guild
{
    [Serializable]
    public class SpecializationId : Key<string>
    {
        public SpecializationId()
        {
        }

        public SpecializationId(string key) : base(key)
        {
        }

        public static implicit operator string(SpecializationId key) => key?.value;
        public static implicit operator SpecializationId(string key) => new(key);
    }
}