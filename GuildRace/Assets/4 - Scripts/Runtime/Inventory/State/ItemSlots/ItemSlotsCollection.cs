using AD.States;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Game.Inventory
{
    public class ItemSlotsCollection : ReactiveCollectionInfo<ItemSlotInfo>, IItemSlotsCollection
    {
        public ItemSlotInfo this[ItemSlot slot] => Values.FirstOrDefault(x => x.Slot == slot);

        public ItemSlotsCollection(IEnumerable<ItemSlotInfo> values) : base(values)
        {
        }
    }
}