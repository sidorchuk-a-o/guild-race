using AD.Services;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace Game.Items
{
    public class ItemsDatabaseService : Service, IItemsDatabaseService
    {
        private readonly ItemsFactory itemsFactory;
        private readonly EquipSlotsFactory equipSlotsFactory;

        public ItemsDatabaseService(ItemsDatabaseConfig config)
        {
            itemsFactory = new(config);
            equipSlotsFactory = new(config, this);
        }

        public override async UniTask<bool> Init()
        {
            return await Inited();
        }

        // == Items ==

        public ItemInfo CreateItemInfo(ItemData data)
        {
            return itemsFactory.CreateInfo(data);
        }

        public ItemSM CreateItemSave(ItemInfo info)
        {
            return itemsFactory.CreateSave(info);
        }

        public ItemInfo ReadItemSave(ItemSM save)
        {
            return itemsFactory.ReadSave(save);
        }

        // == Equip Slot ==

        public EquipSlotInfo CreateSlotInfo(EquipSlotData data)
        {
            return equipSlotsFactory.CreateInfo(data);
        }

        public EquipSlotSM CreateSlotSave(EquipSlotInfo info)
        {
            return equipSlotsFactory.CreateSave(info);
        }

        public EquipSlotInfo ReadSlotSave(EquipSlotSM save)
        {
            return equipSlotsFactory.ReadSave(save);
        }

        // == Equip SLots ==

        public IEnumerable<EquipSlotInfo> CreateDefaultSlots()
        {
            return equipSlotsFactory.CreateDefaultSlots();
        }

        public EquipSlotsSM CreateSlotsSave(IEnumerable<EquipSlotInfo> values)
        {
            return equipSlotsFactory.CreateSave(values);
        }

        public IEnumerable<EquipSlotInfo> ReadSlotsSave(EquipSlotsSM save)
        {
            return equipSlotsFactory.ReadSave(save);
        }
    }
}