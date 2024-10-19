using Newtonsoft.Json;

namespace Game.Items
{
    [JsonObject(MemberSerialization.Fields)]
    public abstract class ItemSM
    {
        [ES3Serializable] protected string id;
        [ES3Serializable] protected string dataId;

        public string DataId => dataId;

        public ItemSM(ItemInfo info)
        {
            id = info.Id;
            dataId = info.DataId;
        }
    }
}