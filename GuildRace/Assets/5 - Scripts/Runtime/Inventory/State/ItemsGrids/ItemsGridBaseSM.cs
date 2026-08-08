using Newtonsoft.Json;

namespace AD.Game.Inventory
{
    [JsonObject(MemberSerialization.Fields)]
    public class ItemsGridBaseSM : ItemsGridSM
    {
        public ItemsGridBaseSM(ItemsGridBaseInfo info, IInventoryFactory inventoryFactory)
            : base(info, inventoryFactory)
        {
        }

        public ItemsGridBaseInfo GetValue(ItemsGridBaseData data, IInventoryFactory inventoryFactory)
        {
            var items = itemsSM.GetCollection(inventoryFactory);

            return new(id, data, items);
        }
    }
}