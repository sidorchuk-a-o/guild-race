using Newtonsoft.Json;

namespace AD.Game.Inventory
{
    [JsonObject(MemberSerialization.Fields)]
    public class EquipItemSM : ItemSM
    {
        public EquipItemSM(EquipItemInfo info) : base(info)
        {
        }

        public EquipItemInfo GetValue(EquipItemData data, EquipItemsFactory equipFactory)
        {
            var info = new EquipItemInfo(id, data);

            boundsSM.ApplyValues(info.Bounds);

            return info;
        }
    }
}