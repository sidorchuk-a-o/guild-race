using Newtonsoft.Json;

namespace Game.Guild
{
    [JsonObject(MemberSerialization.Fields)]
    public class GuildRankSM
    {
        [ES3Serializable] private int id;
        [ES3Serializable] private string name;

        public GuildRankSM(GuildRankInfo info)
        {
            id = info.Id;
            name = info.Name.Value;
        }

        public GuildRankInfo GetValue()
        {
            return new(id, name);
        }
    }
}