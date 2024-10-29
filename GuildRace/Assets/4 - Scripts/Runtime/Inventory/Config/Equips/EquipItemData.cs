using UnityEngine;

namespace Game.Inventory
{
    public class EquipItemData : ItemData
    {
        [SerializeField] private int level;
        [SerializeField] private int power;
        [SerializeField] private Rarity rarity;
        [SerializeField] private EquipType type;

        public int Level => level;
        public int Power => power;
        public Rarity Rarity => rarity;
        public EquipType Type => type;
    }
}