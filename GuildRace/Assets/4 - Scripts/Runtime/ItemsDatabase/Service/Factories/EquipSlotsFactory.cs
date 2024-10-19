using System.Collections.Generic;
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

        public IEnumerable<EquipSlotInfo> CreateDefaultSlots()
        {
            return config.EquipsParams.Slots.Select(CreateInfo);
        }

        public EquipSlotsSM CreateSave(IEnumerable<EquipSlotInfo> values)
        {
            return new EquipSlotsSM(values, database);
        }

        public IEnumerable<EquipSlotInfo> ReadSave(EquipSlotsSM save)
        {
            return save.GetValues(database);
        }
    }
}