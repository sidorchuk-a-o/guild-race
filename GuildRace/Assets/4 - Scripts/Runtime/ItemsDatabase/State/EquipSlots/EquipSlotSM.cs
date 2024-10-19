using Newtonsoft.Json;

namespace Game.Items
{
    [JsonObject(MemberSerialization.Fields)]
    public class EquipSlotSM
    {
        [ES3Serializable] private string slot;
        [ES3Serializable] private ItemSM itemSM;

        public EquipSlotSM(EquipSlotInfo info, IItemsDatabaseService database)
        {
            slot = info.Slot;
            itemSM = database.CreateItemSave(info.Item.Value);
        }

        public EquipSlotInfo GetValue(IItemsDatabaseService database)
        {
            var slotInfo = new EquipSlotInfo(slot);
            var itemInfo = database.ReadItemSave(itemSM);

            slotInfo.SetItem(itemInfo as EquipItemInfo);

            return slotInfo;
        }
    }
}