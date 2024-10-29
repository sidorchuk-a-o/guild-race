using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Inventory
{
    [Serializable]
    public class EquipsParams
    {
        // Items
        [SerializeField] private List<EquipItemData> items;
        [SerializeField] private List<EquipGroupData> groups;
        // Params
        [SerializeField] private SlotParamsForItems slotParams = new();
        [SerializeField] private GridParamsForItems gridParams = new();

        private Dictionary<EquipType, EquipTypeData> typesCache;
        private Dictionary<EquipGroup, EquipGroupData> groupsCache;
        private Dictionary<EquipType, EquipGroupData> groupsByTypeCache;

        public IReadOnlyList<EquipItemData> Items => items;
        public IReadOnlyList<EquipGroupData> Groups => groups;

        public SlotParamsForItems SlotParams => slotParams;
        public GridParamsForItems GridParams => gridParams;

        public EquipTypeData GetType(EquipType type)
        {
            typesCache ??= groups
                .SelectMany(x => x.Types)
                .ToDictionary(x => (EquipType)x.Id, x => x);

            return typesCache[type];
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
    }
}