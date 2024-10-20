using Newtonsoft.Json;

namespace Game.Items
{
    [JsonObject(MemberSerialization.Fields)]
    public class EquipSlotSM
    {
        [ES3Serializable] private string slot;
        [ES3Serializable] private ItemSM itemSM;

        public EquipSlotSM(EquipSlotInfo info, IItemsDatabaseService itemsService)
        {
            slot = info.Slot;
            itemSM = itemsService.CreateItemSave(info.Item.Value);
        }

        public EquipSlotInfo GetValue(IItemsDatabaseService itemsService)
        {
            var slotInfo = new EquipSlotInfo(slot);
            var itemInfo = itemsService.ReadItemSave(itemSM);

            slotInfo.SetItem(itemInfo as EquipItemInfo);

            return slotInfo;
        }
    }
}