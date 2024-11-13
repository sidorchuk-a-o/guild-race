using AD.ToolsCollection;
using System;

namespace Game.Guild
{
    [Serializable]
    public class GuildRankId : Key<string>
    {
        public GuildRankId()
        {
        }

        public GuildRankId(string key) : base(key)
        {
        }

        public static implicit operator string(GuildRankId key) => key?.value;
        public static implicit operator GuildRankId(string key) => new(key);
    }
}