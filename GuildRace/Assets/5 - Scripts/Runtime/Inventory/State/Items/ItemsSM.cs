using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace AD.Game.Inventory
{
    [JsonObject(MemberSerialization.Fields)]
    public class ItemsSM
    {
        [ES3Serializable] private List<ItemSM> values;

        public ItemsSM(IItemsCollection collection, IInventoryFactory inventoryFactory)
        {
            values = collection
                .Select(inventoryFactory.CreateItemSave)
                .ToList();
        }

        public IEnumerable<ItemInfo> GetCollection(IInventoryFactory inventoryFactory)
        {
            return values.Select(inventoryFactory.ReadItemSave);
        }
    }
}