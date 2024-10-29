using AD.States;
using System.Collections.Generic;
using System.Linq;

namespace Game.Guild
{
    public class GuildRanksCollection : ReactiveCollectionInfo<GuildRankInfo>, IGuildRanksCollection
    {
        public GuildRankInfo this[GuildRankId id] => Values.FirstOrDefault(x => x.Id == id);

        public GuildRankInfo GuildMaster => this[0];

        public GuildRanksCollection(IEnumerable<GuildRankInfo> values) : base(values)
        {
        }
    }
}