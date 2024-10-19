using System;
using System.Collections.Generic;
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

        public IReadOnlyList<EquipItemData> Items => items;
        public IReadOnlyList<EquipSlotData> Slots => slots;
        public IReadOnlyList<EquipGroupData> Groups => groups;
        public IReadOnlyList<RarityData> Rarities => rarities;
    }
}