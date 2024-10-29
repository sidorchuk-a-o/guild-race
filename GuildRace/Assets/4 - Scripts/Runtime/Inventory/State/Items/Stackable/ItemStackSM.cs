using Newtonsoft.Json;

namespace Game.Inventory
{
    [JsonObject(MemberSerialization.Fields)]
    public class ItemStackSM
    {
        [ES3Serializable] private int value;

        public ItemStackSM(ItemStackInfo info)
        {
            value = info.Value;
        }

        public void ApplyValues(ItemStackInfo info)
        {
            info.SetValue(value);
        }
    }
}