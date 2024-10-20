namespace Game.Items
{
    public interface IItemsDatabaseService
    {
        ItemInfo CreateItemInfo(ItemData data);
        ItemSM CreateItemSave(ItemInfo info);
        ItemInfo ReadItemSave(ItemSM save);

        EquipSlotInfo CreateSlotInfo(EquipSlotData data);
        EquipSlotSM CreateSlotSave(EquipSlotInfo info);
        EquipSlotInfo ReadSlotSave(EquipSlotSM save);

        IEquipSlotsCollection CreateDefaultSlots();
        EquipSlotsSM CreateSlotsSave(IEquipSlotsCollection values);
        IEquipSlotsCollection ReadSlotsSave(EquipSlotsSM save);
    }
}