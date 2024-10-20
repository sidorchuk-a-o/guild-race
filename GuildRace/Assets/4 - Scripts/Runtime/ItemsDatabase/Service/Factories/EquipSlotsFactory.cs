using System.Linq;

namespace Game.Items
{
    public class EquipSlotsFactory
    {
        private readonly ItemsDatabaseConfig config;
        private readonly IItemsDatabaseService itemsService;

        public EquipSlotsFactory(ItemsDatabaseConfig config, IItemsDatabaseService itemsService)
        {
            this.config = config;
            this.itemsService = itemsService;
        }

        // == Slot ==

        public EquipSlotInfo CreateInfo(EquipSlotData data)
        {
            return new EquipSlotInfo(data.Id);
        }

        public EquipSlotSM CreateSave(EquipSlotInfo info)
        {
            return new EquipSlotSM(info, itemsService);
        }

        public EquipSlotInfo ReadSave(EquipSlotSM save)
        {
            return save.GetValue(itemsService);
        }

        // == Slots ==

        public IEquipSlotsCollection CreateDefaultSlots()
        {
            var slots = config.EquipsParams.Slots.Select(CreateInfo);
            var slotsCollection = new EquipSlotsCollection(slots);

            slotsCollection.Init();

            return slotsCollection;
        }

        public EquipSlotsSM CreateSave(IEquipSlotsCollection values)
        {
            return new EquipSlotsSM(values, itemsService);
        }

        public IEquipSlotsCollection ReadSave(EquipSlotsSM save)
        {
            var slots = save.GetValues(itemsService);
            var slotsCollection = new EquipSlotsCollection(slots);

            slotsCollection.Init();

            return slotsCollection;
        }
    }
}