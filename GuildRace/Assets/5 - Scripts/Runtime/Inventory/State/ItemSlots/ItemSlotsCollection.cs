using System.Collections.Generic;
using System.Linq;
using AD.ToolsCollection;

namespace AD.Game.Inventory
{
    public class ItemSlotsCollection : ReactiveCollection<ItemSlotInfo>, IItemSlotsCollection
    {
        public ItemSlotInfo this[ItemSlot slot] => Values.FirstOrDefault(x => x.Slot == slot);

        public ItemSlotsCollection(IEnumerable<ItemSlotInfo> values) : base(values)
        {
        }
    }
}