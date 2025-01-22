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
        [SerializeField] private List<RecyclingRarityModData> rarityMods;

        private Dictionary<Rarity, RecyclingRarityModData> rarityModCache;

        public RecycleSlotData RecycleSlot => recycleSlot;

        public RecyclingRarityModData GetRarityMod(Rarity rarity)
        {
            rarityModCache ??= rarityMods.ToDictionary(x => x.Rarity, x => x);
            rarityModCache.TryGetValue(rarity, out var data);

            return data ?? rarityMods[0];
        }
    }
}