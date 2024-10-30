using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Game.Inventory
{
    [JsonObject(MemberSerialization.Fields)]
    public class ItemsSM
    {
        [ES3Serializable] private List<ItemSM> values;

        public ItemsSM(IItemsCollection collection, IInventoryFactory inventoryFactory)
        {
            values = collection
                .Select(x => inventoryFactory.CreateItemSave(x))
                .ToList();
        }

        public IEnumerable<ItemInfo> GetCollection(IInventoryFactory inventoryFactory)
        {
            return values.Select(x => inventoryFactory.ReadItemSave(x));
        }
    }
}