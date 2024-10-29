using Newtonsoft.Json;

namespace Game.Inventory
{
    [JsonObject(MemberSerialization.Fields)]
    public class ReagentItemSM : ItemSM
    {
        [ES3NonSerializable] private ItemStackSM stackSM;

        public ReagentItemSM(ReagentItemInfo info) : base(info)
        {
            stackSM = new(info.Stack);
        }

        public ReagentItemInfo GetValue(ReagentItemData data)
        {
            var info = new ReagentItemInfo(id, data);

            stackSM.ApplyValues(info.Stack);

            return info;
        }
    }
}