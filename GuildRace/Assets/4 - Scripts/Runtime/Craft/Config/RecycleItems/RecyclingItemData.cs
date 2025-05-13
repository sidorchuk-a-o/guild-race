using Game.Inventory;
using System;
using UnityEngine;

namespace Game.Craft
{
    [Serializable]
    public class RecyclingItemData
    {
        [SerializeField] private Rarity rarity;
        [SerializeField] private float percent;

        public Rarity Rarity => rarity;
        public float Percent => percent;
    }
}