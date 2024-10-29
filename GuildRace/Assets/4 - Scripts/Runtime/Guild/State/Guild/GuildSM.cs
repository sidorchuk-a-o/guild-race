﻿using Game.Inventory;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Game.Guild
{
    [JsonObject(MemberSerialization.Fields)]
    public class GuildSM
    {
        public const string key = "guild";

        [ES3Serializable] private string name;
        [ES3Serializable] private CharactersSM charactersSM;
        [ES3Serializable] private GuildRanksSM guildRanksSM;
        [ES3Serializable] private GuildBankTabsSM guildBankTabsSM;

        public string Name
        {
            get => name;
            set => name = value;
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