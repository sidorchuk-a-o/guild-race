using Newtonsoft.Json;

namespace Game.Items
{
    [JsonObject(MemberSerialization.Fields)]
    public class EquipItemSM : ItemSM
    {
        public EquipItemSM(EquipItemInfo info) : base(info)
        {
        }

        public EquipItemInfo GetValue(EquipItemData data)
        {
            return new EquipItemInfo(id, data);
        }
    }
}