using System.Collections.Generic;
using System.Linq;
using AD.Game.Inventory;
using Newtonsoft.Json;

namespace AD.Game.Instances
{
    [JsonObject(MemberSerialization.Fields)]
    public class ActiveInstancesSM
    {
        [ES3Serializable] private List<ActiveInstanceSM> values;

        public ActiveInstancesSM(IEnumerable<ActiveInstanceInfo> values, IInventoryService inventoryService)
        {
            this.values = values
                .Select(x => new ActiveInstanceSM(x, inventoryService))
                .ToList();
        }

        public IEnumerable<ActiveInstanceInfo> GetValues(IInventoryService inventoryService, SeasonsCollection seasons)
        {
            return values.Select(x => x.GetValue(inventoryService, seasons));
        }
    }
}