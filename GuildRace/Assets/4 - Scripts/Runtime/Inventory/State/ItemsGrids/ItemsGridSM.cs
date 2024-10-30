using Newtonsoft.Json;

namespace Game.Inventory
{
    [JsonObject(MemberSerialization.Fields)]
    public class ItemsGridSM
    {
        [ES3Serializable] private string id;
        [ES3Serializable] private string dataId;
        [ES3Serializable] private ItemsSM itemsSM;

        public string Id => id;
        public string DataId => dataId;

        public ItemsGridSM(ItemsGridInfo info, IInventoryFactory inventoryFactory)
        {
            id = info.Id;
            dataId = info.DataId;
            itemsSM = new(info.Items, inventoryFactory);
        }

        public ItemsGridInfo GetValue(InventoryConfig config, IInventoryFactory inventoryFactory)
        {
            var data = config.GetGrid(dataId);
            var items = itemsSM.GetCollection(inventoryFactory);

            return new ItemsGridInfo(id, data, items);
        }
    }
}