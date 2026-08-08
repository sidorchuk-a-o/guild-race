using Newtonsoft.Json;

namespace AD.Game.Inventory
{
    [JsonObject(MemberSerialization.Fields)]
    public abstract class ItemSlotSM
    {
        [ES3Serializable] protected string id;
        [ES3Serializable] protected int dataId;
        [ES3Serializable] protected ItemSM itemSM;

        public int DataId => dataId;

        protected ItemSlotSM(ItemSlotInfo info, IInventoryFactory inventoryFactory)
        {
            id = info.Id;
            dataId = info.DataId;
            itemSM = inventoryFactory.CreateItemSave(info.Item.CurrentValue);
        }
    }
}