using System.Linq;

namespace Game.Items
{
    public class EquipSlotsFactory
    {
        private readonly ItemsDatabaseConfig config;
        private readonly IItemsDatabaseService database;

        public EquipSlotsFactory(ItemsDatabaseConfig config, IItemsDatabaseService database)
        {
            this.config = config;
            this.database = database;
        }

        // == Slot ==

        public EquipSlotInfo CreateInfo(EquipSlotData data)
        {
            return new EquipSlotInfo(data.Id);
        }

        public EquipSlotSM CreateSave(EquipSlotInfo info)
        {
            return new EquipSlotSM(info, database);
        }

        public EquipSlotInfo ReadSave(EquipSlotSM save)
        {
            return save.GetValue(database);
        }

        // == Slots ==

        public IEquipSlotsCollection CreateDefaultSlots()
        {
            var slots = config.EquipsParams.Slots.Select(CreateInfo);

            return new EquipSlotsCollection(slots);
        }

        public EquipSlotsSM CreateSave(IEquipSlotsCollection values)
        {
            return new EquipSlotsSM(values, database);
        }

        public IEquipSlotsCollection ReadSave(EquipSlotsSM save)
        {
            var slots = save.GetValues(database);

            return new EquipSlotsCollection(slots);
        }
    }
}