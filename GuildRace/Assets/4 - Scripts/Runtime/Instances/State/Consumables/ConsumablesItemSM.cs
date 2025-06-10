using Game.Inventory;
using Newtonsoft.Json;

namespace Game.Instances
{
    [JsonObject(MemberSerialization.Fields)]
    public class ConsumablesItemSM : ItemSM
    {
        [ES3Serializable] private ItemStackSM stackSM;

        public ConsumablesItemSM(ConsumablesItemInfo info) : base(info)
        {
            stackSM = new(info.Stack);
        }

        public ConsumablesItemInfo GetValue(ConsumablesItemData data, ConsumablesItemsFactory consumablesIF)
        {
            var info = new ConsumablesItemInfo(id, data, consumablesIF.ItemType);

            stackSM.ApplyValues(info.Stack);
            boundsSM.ApplyValues(info.Bounds);

            return info;
        }
    }
}