using Game.Inventory;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Game.Guild
{
    [JsonObject(MemberSerialization.Fields)]
    public class GuildSM
    {
        public const string key = "guild";

        [ES3Serializable] private string guildName;
        [ES3Serializable] private string playerName;
        [ES3Serializable] private CharactersSM charactersSM;
        [ES3Serializable] private GuildRanksSM guildRanksSM;
        [ES3Serializable] private GuildBankTabsSM guildBankTabsSM;
        [ES3Serializable] private EmblemSM emblemSM;

        public string GuildName
        {
            get => guildName;
            set => guildName = value;
        }

        public string PlayerName
        {
            get => playerName;
            set => playerName = value;
        }

        public EmblemInfo Emblem
        {
            get => emblemSM.GetValue();
            set => emblemSM = new(value);
        }

        public IEnumerable<GuildRankInfo> GuildRanks
        {
            get => guildRanksSM.GetValues();
            set => guildRanksSM = new(value);
        }

        public void SetCharacters(IEnumerable<CharacterInfo> value, IInventoryService inventoryService)
        {
            charactersSM = new(value, inventoryService);
        }

        public IEnumerable<CharacterInfo> GetCharacters(IInventoryService inventoryService)
        {
            return charactersSM.GetValues(inventoryService);
        }

        public void SetBankTabs(IEnumerable<GuildBankTabInfo> values, IInventoryService inventoryService)
        {
            guildBankTabsSM = new(values, inventoryService);
        }

        public IEnumerable<GuildBankTabInfo> GetBankTabs(GuildConfig config, IInventoryService inventoryService)
        {
            return guildBankTabsSM.GetCollection(config, inventoryService);
        }
    }
}