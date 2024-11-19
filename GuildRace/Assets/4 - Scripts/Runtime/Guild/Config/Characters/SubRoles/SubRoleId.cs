using AD.ToolsCollection;
using System;

namespace Game.Guild
{
    [Serializable]
    public class SubRoleId : Key<int>
    {
        public SubRoleId()
        {
        }

        public SubRoleId(int key) : base(key)
        {
        }

        public static implicit operator int(SubRoleId key) => key?.value ?? -1;
        public static implicit operator SubRoleId(int key) => new(key);
    }
}