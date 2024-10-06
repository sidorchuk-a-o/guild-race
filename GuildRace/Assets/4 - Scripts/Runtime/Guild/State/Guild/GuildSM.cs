using Newtonsoft.Json;

namespace Game.Guild
{
    [JsonObject(MemberSerialization.Fields)]
    public class GuildSM
    {
        public const string key = "guild";

        [ES3Serializable] private string name;

        public string Name
        {
            get => name;
            set => name = value;
        }
    }
}