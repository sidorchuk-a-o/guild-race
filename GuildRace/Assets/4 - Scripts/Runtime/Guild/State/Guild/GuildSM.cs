using Game.Items;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Game.Guild
{
    [JsonObject(MemberSerialization.Fields)]
    public class GuildSM
    {
        public const string key = "guild";

        [ES3Serializable] private string name;
        [ES3Serializable] private CharactersSM characters;
        [ES3Serializable] private GuildRanksSM guildRanks;

        public string Name
        {
            get => name;
            set => name = value;
        }

        public IEnumerable<GuildRankInfo> GuildRanks
        {
            get => guildRanks.GetValues();
            set => guildRanks = new(value);
        }

        public void SetCharacters(IEnumerable<CharacterInfo> value, IItemsDatabaseService itemsService)
        {
            characters = new(value, itemsService);
        }

        public IEnumerable<CharacterInfo> GetCharacters(IItemsDatabaseService itemsService)
        {
            return characters.GetValues(itemsService);
        }
    }
}