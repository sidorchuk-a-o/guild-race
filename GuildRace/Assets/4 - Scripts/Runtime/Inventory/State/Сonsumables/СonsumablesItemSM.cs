using Newtonsoft.Json;

namespace Game.Inventory
{
    [JsonObject(MemberSerialization.Fields)]
    public class СonsumablesItemSM : ItemSM
    {
        [ES3Serializable] private ItemStackSM stackSM;

        public СonsumablesItemSM(СonsumablesItemInfo info) : base(info)
        {
            stackSM = new(info.Stack);
        }

        public СonsumablesItemInfo GetValue(СonsumablesItemData data)
        {
            var info = new СonsumablesItemInfo(id, data);

            stackSM.ApplyValues(info.Stack);
            boundsSM.ApplyValues(info.Bounds);

            return info;
        }
    }
}