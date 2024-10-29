using Newtonsoft.Json;

namespace Game.Inventory
{
    [JsonObject(MemberSerialization.Fields)]
    public class ItemSlotSM
    {
        [ES3Serializable] private string id;
        [ES3Serializable] private string slot;
        [ES3Serializable] private ItemSM itemSM;

        public ItemSlotSM(ItemSlotInfo info, IInventoryFactory inventoryFactory)
        {
            id = info.Id;
            slot = info.Slot;
            itemSM = inventoryFactory.CreateItemSave(info.Item.Value);
        }

        public ItemSlotInfo GetValue(InventoryConfig config, IInventoryFactory inventoryFactory)
        {
            var slotData = config.ItemSlotsParams.GetSlot(slot);
            var slotInfo = new ItemSlotInfo(id, slotData);

            var itemInfo = inventoryFactory.ReadItemSave(itemSM);

            slotInfo.SetItem(itemInfo);

            return slotInfo;
        }
    }
}