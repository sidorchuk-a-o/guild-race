using System.Collections.Generic;
using System.Linq;
using AD.ToolsCollection;

namespace AD.Game.Guild
{
    public class GuildRanksCollection : ReactiveCollection<GuildRankInfo>, IGuildRanksCollection
    {
        public GuildRankInfo this[GuildRankId id] => Values.FirstOrDefault(x => x.Id == id);

        public GuildRankInfo GuildMaster => this[index: 0];

        public GuildRanksCollection(IEnumerable<GuildRankInfo> values) : base(values)
        {
        }

        public int IndexOf(GuildRankId rankId)
        {
            return FindIndex(x => x.Id == rankId);
        }
    }
}