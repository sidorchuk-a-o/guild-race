using Newtonsoft.Json;

namespace AD.Game.Inventory
{
    [JsonObject(MemberSerialization.Fields)]
    public abstract class ItemSM
    {
        [ES3Serializable] protected string id;
        [ES3Serializable] protected int dataId;
        [ES3Serializable] protected ItemBoundsSM boundsSM;

        public int DataId => dataId;

        protected ItemSM(ItemInfo info)
        {
            id = info.Id;
            dataId = info.DataId;
            boundsSM = new(info.Bounds);
        }
    }
}