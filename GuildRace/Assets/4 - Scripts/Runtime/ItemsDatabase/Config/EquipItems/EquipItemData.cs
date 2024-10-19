using System.Collections.Generic;
using UnityEngine;

namespace Game.Items
{
    public class EquipItemData : ItemData
    {
        [SerializeField] private int level;
        [SerializeField] private int power;
        [SerializeField] private Rarity rarity;
        [SerializeField] private EquipType type;
        [SerializeField] private EquipSlot slot;
        [SerializeField] private List<ItemSourceKey> sources;

        public int Level => level;
        public int Power => power;
        public Rarity Rarity => rarity;
        public EquipType Type => type;
        public EquipSlot Slot => slot;
        public IReadOnlyList<ItemSourceKey> Sources => sources;
    }
}