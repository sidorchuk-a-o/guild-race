using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace AD.Game.Inventory
{
    [JsonObject(MemberSerialization.Fields)]
    public class ItemSlotsSM
    {
        [ES3Serializable] private List<ItemSlotSM> values;

        public ItemSlotsSM(IEnumerable<ItemSlotInfo> values, IInventoryFactory inventoryFactory)
        {
            this.values = values
                .Select(inventoryFactory.CreateSlotSave)
                .ToList();
        }

        public IEnumerable<ItemSlotInfo> GetValues(IInventoryFactory inventoryFactory)
        {
            return values.Select(inventoryFactory.ReadSlotSave);
        }
    }
}