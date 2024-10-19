using System.Collections.Generic;

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

        IEnumerable<EquipSlotInfo> CreateDefaultSlots();
        EquipSlotsSM CreateSlotsSave(IEnumerable<EquipSlotInfo> values);
        IEnumerable<EquipSlotInfo> ReadSlotsSave(EquipSlotsSM save);
    }
}