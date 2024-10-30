using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Game.Guild
{
    [JsonObject(MemberSerialization.Fields)]
    public class GuildRanksSM
    {
        [ES3Serializable] private List<GuildRankSM> values;

        public GuildRanksSM(IEnumerable<GuildRankInfo> values)
        {
            this.values = values
                .Select(x => new GuildRankSM(x))
                .ToList();
        }

        public IEnumerable<GuildRankInfo> GetValues()
        {
            return values.Select(x => x.GetValue());
        }
    }
}