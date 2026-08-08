using Newtonsoft.Json;

namespace AD.Game.Inventory
{
    [JsonObject(MemberSerialization.Fields)]
    public abstract class ItemsGridSM
    {
        [ES3Serializable] protected string id;
        [ES3Serializable] protected int dataId;
        [ES3Serializable] protected ItemsSM itemsSM;

        public int DataId => dataId;

        protected ItemsGridSM(ItemsGridInfo info, IInventoryFactory inventoryFactory)
        {
            id = info.Id;
            dataId = info.DataId;
            itemsSM = new(info.Items, inventoryFactory);
        }
    }
}