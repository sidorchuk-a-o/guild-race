using Game.Inventory;
using Newtonsoft.Json;

namespace Game.Craft
{
    [JsonObject(MemberSerialization.Fields)]
    public class ReagentItemSM : ItemSM
    {
        [ES3Serializable] private ItemStackSM stackSM;

        public ReagentItemSM(ReagentItemInfo info) : base(info)
        {
            stackSM = new(info.Stack);
        }

        public ReagentItemInfo GetValue(ReagentItemData data, ReagentItemsFactory reagentIF)
        {
            var info = new ReagentItemInfo(id, data);

            stackSM.ApplyValues(info.Stack);
            boundsSM.ApplyValues(info.Bounds);

            return info;
        }
    }
}