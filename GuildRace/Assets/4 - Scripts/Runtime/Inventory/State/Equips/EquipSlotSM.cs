using Newtonsoft.Json;

namespace Game.Inventory
{
    [JsonObject(MemberSerialization.Fields)]
    public class EquipSlotSM : ItemSlotSM
    {
        [ES3Serializable] private int equipType;

        public EquipSlotSM(EquipSlotInfo info, IInventoryFactory inventoryFactory)
            : base(info, inventoryFactory)
        {
            equipType = info.EquipType;
        }

        public EquipSlotInfo GetValue(EquipSlotData data, IInventoryFactory inventoryFactory)
        {
            var slotInfo = new EquipSlotInfo(id, data);
            var itemInfo = inventoryFactory.ReadItemSave(itemSM);

            slotInfo.SetItem(itemInfo);
            slotInfo.SetEquipType(equipType);

            return slotInfo;
        }
    }
}