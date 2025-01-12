using AD.ToolsCollection;
using System;

namespace Game.Guild
{
    [Serializable]
    public class ClassId : Key<int>
    {
        public ClassId()
        {
        }

        public ClassId(int key) : base(key)
        {
        }

        public static implicit operator int(ClassId key) => key?.value ?? -1;
        public static implicit operator ClassId(int key) => new(key);
    }
}