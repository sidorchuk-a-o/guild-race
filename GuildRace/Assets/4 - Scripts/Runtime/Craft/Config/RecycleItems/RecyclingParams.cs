using Game.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Craft
{
    [Serializable]
    public class RecyclingParams
    {
        [SerializeField] private RecycleSlotData recycleSlot;
        [SerializeField] private List<RecyclingItemData> recyclingItems;
        [SerializeField] private List<RecyclingReagentData> recyclingReagents;
        [SerializeField] private List<int> ignoreReagents;

        private Dictionary<Rarity, RecyclingItemData> recyclingItemsCache;
        private Dictionary<Rarity, RecyclingReagentData> recyclingReagentCache;

        public RecycleSlotData RecycleSlot => recycleSlot;
        public IReadOnlyCollection<int> IgnoreReagents => ignoreReagents;

        public RecyclingItemData GetRecyclingItem(Rarity rarity)
        {
            recyclingItemsCache ??= recyclingItems.ToDictionary(x => x.Rarity, x => x);
            recyclingItemsCache.TryGetValue(rarity, out var data);

            return data ?? recyclingItems[0];
        }

        public RecyclingReagentData GetRecyclingReagent(Rarity rarity)
        {
            recyclingReagentCache ??= recyclingReagents.ToDictionary(x => x.Rarity, x => x);
            recyclingReagentCache.TryGetValue(rarity, out var data);

            return data ?? recyclingReagents[0];
        }
    }
}