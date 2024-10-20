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
        [ES3Serializable] private RecruitingSM recruiting;

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

        public RecruitingSM Recruiting
        {
            get => recruiting;
            set => recruiting = value;
        }

        public void SetCharacters(IEnumerable<CharacterInfo> value, IItemsDatabaseService itemsDatabase)
        {
            characters = new(value, itemsDatabase);
        }

        public IEnumerable<CharacterInfo> GetCharacters(IItemsDatabaseService itemsDatabase)
        {
            return characters.GetValues(itemsDatabase);
        }
    }
}