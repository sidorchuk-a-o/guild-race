using Newtonsoft.Json;

namespace Game.Inventory
{
    [JsonObject(MemberSerialization.Fields)]
    public class EquipItemSM : ItemSM
    {
        public EquipItemSM(EquipItemInfo info) : base(info)
        {
        }

        public EquipItemInfo GetValue(EquipItemData data, EquipItemsFactory equipIF)
        {
            var info = new EquipItemInfo(id, data);

            boundsSM.ApplyValues(info.Bounds);

            return info;
        }
    }
}