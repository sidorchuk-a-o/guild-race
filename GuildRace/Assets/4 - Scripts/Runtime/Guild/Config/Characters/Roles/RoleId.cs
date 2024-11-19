using AD.ToolsCollection;
using System;

namespace Game.Guild
{
    [Serializable]
    public class RoleId : Key<int>
    {
        public RoleId()
        {
        }

        public RoleId(int key) : base(key)
        {
        }

        public static implicit operator int(RoleId key) => key?.value ?? -1;
        public static implicit operator RoleId(int key) => new(key);
    }
}