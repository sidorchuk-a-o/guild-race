using AD.ToolsCollection;
using System;

namespace Game.Guild
{
    [Serializable]
    public class RoleId : Key<string>
    {
        public RoleId()
        {
        }

        public RoleId(string key) : base(key)
        {
        }

        public static implicit operator string(RoleId key) => key?.value;
        public static implicit operator RoleId(string key) => new(key);
    }
}