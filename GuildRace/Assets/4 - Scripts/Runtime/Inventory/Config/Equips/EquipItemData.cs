using UnityEngine;

namespace Game.Inventory
{
    public class EquipItemData : ItemData
    {
        [SerializeField] private int level;
        [SerializeField] private EquipType type;
        [SerializeField] private Rarity rarity;
        [SerializeField] private CharacterParams characterParams = new();

        public int Level => level;
        public EquipType Type => type;
        public Rarity Rarity => rarity;
        public CharacterParams CharacterParams => characterParams;
    }
}