using System;
using AD.ToolsCollection;

namespace AD.Game.Guild
{
    [Serializable]
    public class GuildRankId : Key<int>
    {
        public GuildRankId()
        {
        }

        public GuildRankId(int key) : base(key)
        {
        }

        public static implicit operator int(GuildRankId key) => key?.value ?? -1;
        public static implicit operator GuildRankId(int key) => new(key);
    }
}