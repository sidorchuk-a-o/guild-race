using AD.Services;
using Cysharp.Threading.Tasks;

namespace Game.Items
{
    public class ItemsService : Service, IItemsService
    {
        private readonly ItemsFactory itemsFactory;
        private readonly EquipSlotsFactory equipSlotsFactory;

        public ItemsService(ItemsConfig config)
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

        public IEquipSlotsCollection CreateDefaultSlots()
        {
            return equipSlotsFactory.CreateDefaultSlots();
        }

        public EquipSlotsSM CreateSlotsSave(IEquipSlotsCollection values)
        {
            return equipSlotsFactory.CreateSave(values);
        }

        public IEquipSlotsCollection ReadSlotsSave(EquipSlotsSM save)
        {
            return equipSlotsFactory.ReadSave(save);
        }
    }
}