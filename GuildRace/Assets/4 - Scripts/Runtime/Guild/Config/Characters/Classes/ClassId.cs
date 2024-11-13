using AD.ToolsCollection;
using System;

namespace Game.Guild
{
    [Serializable]
    public class ClassId : Key<string>
    {
        public ClassId()
        {
        }

        public ClassId(string key) : base(key)
        {
        }

        public static implicit operator string(ClassId key) => key?.value;
        public static implicit operator ClassId(string key) => new(key);
    }
}