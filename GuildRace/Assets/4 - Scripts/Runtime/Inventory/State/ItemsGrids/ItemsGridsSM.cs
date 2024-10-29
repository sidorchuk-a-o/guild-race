using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Game.Inventory
{
    [JsonObject(MemberSerialization.Fields)]
    public class ItemsGridsSM
    {
        [ES3Serializable] private List<ItemsGridSM> values;

        public ItemsGridsSM(IEnumerable<ItemsGridInfo> collection, IInventoryFactory inventoryFactory)
        {
            values = collection
                .Select(x => inventoryFactory.CreateGridSave(x))
                .ToList();
        }

        public IEnumerable<ItemsGridInfo> GetCollection(IInventoryFactory inventoryFactory)
        {
            return values.Select(x => inventoryFactory.ReadGridSave(x));
        }
    }
}