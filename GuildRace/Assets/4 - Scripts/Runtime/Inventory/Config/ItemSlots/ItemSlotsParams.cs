using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Inventory
{
    [Serializable]
    public class ItemSlotsParams
    {
        [SerializeField] private List<ItemSlotData> slots;

        private Dictionary<ItemSlot, ItemSlotData> slotsCache;

        public IReadOnlyList<ItemSlotData> Slots => slots;

        public ItemSlotData GetSlot(ItemSlot slot)
        {
            slotsCache ??= slots.ToDictionary(x => (ItemSlot)x.Id, x => x);

            return slotsCache[slot];
        }
    }
}