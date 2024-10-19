using AD.States;
using System.Collections.Generic;
using System.Linq;

namespace Game.Items
{
    public class EquipSlotsCollection : ReactiveCollectionInfo<EquipSlotInfo>, IEquipSlotsCollection
    {
        public EquipSlotInfo this[EquipSlot slot] => Values.FirstOrDefault(x => x.Slot == slot);

        public EquipSlotsCollection(IEnumerable<EquipSlotInfo> values) : base(values)
        {
        }
    }
}