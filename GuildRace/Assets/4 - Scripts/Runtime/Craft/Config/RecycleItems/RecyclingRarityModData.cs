using Game.Inventory;
using System;
using UnityEngine;

namespace Game.Craft
{
    [Serializable]
    public class RecyclingRarityModData
    {
        [SerializeField] private Rarity rarity;
        [SerializeField] private int value;

        public Rarity Rarity => rarity;
        public int Value => value;
    }
}