using System.Collections.Generic;
using System.Linq;
using AD.Game.Inventory;
using Newtonsoft.Json;

namespace AD.Game.Guild
{
    [JsonObject(MemberSerialization.Fields)]
    public class JoinRequestsSM
    {
        [ES3Serializable] private List<JoinRequestSM> values;

        public JoinRequestsSM(IEnumerable<JoinRequestInfo> values, IInventoryService inventoryService)
        {
            this.values = values
                .Select(x => new JoinRequestSM(x, inventoryService))
                .ToList();
        }

        public IEnumerable<JoinRequestInfo> GetValues(IInventoryService inventoryService)
        {
            return values.Select(x => x.GetValue(inventoryService));
        }
    }
}