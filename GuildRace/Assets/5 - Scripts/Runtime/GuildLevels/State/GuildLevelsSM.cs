using System.Collections.Generic;
using Newtonsoft.Json;
using VContainer;

namespace AD.Game.GuildLevels
{
    [JsonObject(MemberSerialization.Fields)]
    public class GuildLevelsSM
    {
        [ES3Serializable] private LevelsSM levelsSM;

        public IEnumerable<LevelInfo> GuildLevels
        {
            set => levelsSM = new(value);
        }

        public IEnumerable<LevelInfo> GetGuildLevels(GuildLevelsConfig config, IObjectResolver resolver)
        {
            return levelsSM.GetValues(config, resolver);
        }
    }
}