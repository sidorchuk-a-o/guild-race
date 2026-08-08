using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace AD.Game.Inventory
{
    [JsonObject(MemberSerialization.Fields)]
    public class ItemsGridsSM
    {
        [ES3Serializable] private List<ItemsGridSM> values;

        public ItemsGridsSM(IEnumerable<ItemsGridInfo> collection, IInventoryFactory inventoryFactory)
        {
            values = collection
                .Select(inventoryFactory.CreateGridSave)
                .ToList();
        }

        public IEnumerable<ItemsGridInfo> GetCollection(IInventoryFactory inventoryFactory)
        {
            return values.Select(inventoryFactory.ReadGridSave);
        }
    }
}