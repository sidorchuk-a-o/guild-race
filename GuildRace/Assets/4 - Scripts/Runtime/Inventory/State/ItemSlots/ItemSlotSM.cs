using Newtonsoft.Json;

namespace Game.Inventory
{
    [JsonObject(MemberSerialization.Fields)]
    public abstract class ItemSlotSM
    {
        [ES3Serializable] protected string id;
        [ES3Serializable] protected string dataId;
        [ES3Serializable] protected ItemSM itemSM;

        public string DataId => dataId;

        public ItemSlotSM(ItemSlotInfo info, IInventoryFactory inventoryFactory)
        {
            id = info.Id;
            dataId = info.DataId;
            itemSM = inventoryFactory.CreateItemSave(info.Item.Value);
        }
    }
}