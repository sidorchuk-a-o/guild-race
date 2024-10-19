using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Items
{
    [Serializable]
    public class EquipsParams
    {
        [SerializeField] private List<EquipItemData> items;
        [SerializeField] private List<EquipSlotData> slots;
        [SerializeField] private List<EquipGroupData> groups;
        [SerializeField] private List<RarityData> rarities;

        private Dictionary<string, EquipItemData> itemsCache;
        private Dictionary<EquipSlot, EquipSlotData> slotsCache;
        private Dictionary<EquipGroup, EquipGroupData> groupsCache;
        private Dictionary<EquipType, EquipGroupData> groupsByTypeCache;
        private Dictionary<EquipType, EquipTypeData> typesCache;
        private Dictionary<Rarity, RarityData> raritiesCache;

        public IReadOnlyList<EquipItemData> Items => items;
        public IReadOnlyList<EquipSlotData> Slots => slots;
        public IReadOnlyList<EquipGroupData> Groups => groups;
        public IReadOnlyList<RarityData> Rarities => rarities;

        public EquipItemData GetItem(string id)
        {
            itemsCache ??= items.ToDictionary(x => x.Id, x => x);

            return itemsCache[id];
        }

        public EquipSlotData GetSlot(EquipSlot slot)
        {
            slotsCache ??= slots.ToDictionary(x => (EquipSlot)x.Id, x => x);

            return slotsCache[slot];
        }

        public EquipGroupData GetGroup(EquipGroup group)
        {
            groupsCache ??= groups.ToDictionary(x => (EquipGroup)x.Id, x => x);

            return groupsCache[group];
        }

        public EquipGroupData GetGroup(EquipType type)
        {
            groupsByTypeCache ??= groups
                .SelectMany(group => group.Types.Select(type => (type, group)))
                .ToDictionary(x => (EquipType)x.type.Id, x => x.group);

            return groupsByTypeCache[type];
        }

        public EquipTypeData GetType(EquipType type)
        {
            typesCache ??= groups
                .SelectMany(x => x.Types)
                .ToDictionary(x => (EquipType)x.Id, x => x);

            return typesCache[type];
        }

        public RarityData GetRarity(Rarity rarity)
        {
            raritiesCache ??= rarities.ToDictionary(x => (Rarity)x.Id, x => x);

            return raritiesCache[rarity];
        }
    }
}