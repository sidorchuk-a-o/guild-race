using Game.Inventory;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Game.Guild
{
    [JsonObject(MemberSerialization.Fields)]
    public class GuildBankTabsSM
    {
        [ES3Serializable] private List<GuildBankTabSM> values;

        public GuildBankTabsSM(IEnumerable<GuildBankTabInfo> values, IInventoryService inventoryService)
        {
            this.values = values
                .Select(x => new GuildBankTabSM(x, inventoryService))
                .ToList();
        }

        public IEnumerable<GuildBankTabInfo> GetCollection(GuildConfig config, IInventoryService inventoryService)
        {
            return values.Select(x => x.GetValue(config, inventoryService));
        }
    }
}