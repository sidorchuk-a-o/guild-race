using AD.ToolsCollection;
using System;

namespace Game.Guild
{
    [Serializable]
    public class ResourceId : Key<int>
    {
        public ResourceId()
        {
        }

        public ResourceId(int key) : base(key)
        {
        }

        public static implicit operator int(ResourceId key) => key?.value ?? -1;
        public static implicit operator ResourceId(int key) => new(key);
    }
}