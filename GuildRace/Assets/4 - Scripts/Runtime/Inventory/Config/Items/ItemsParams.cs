using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Inventory
{
    [Serializable]
    public class ItemsParams
    {
        [SerializeField] private List<RarityData> rarities;
        [SerializeField] private List<ItemsFactory> factories;

        private Dictionary<Rarity, RarityData> raritiesCache;

        public IReadOnlyList<RarityData> Rarities => rarities;
        public IReadOnlyList<ItemsFactory> Factories => factories;

        public RarityData GetRarity(Rarity rarity)
        {
            raritiesCache ??= rarities.ToDictionary(x => (Rarity)x.Id, x => x);

            return raritiesCache[rarity];
        }
    }
}