using System;
using AD.Game.Inventory;
using UnityEngine;

namespace AD.Game.Craft
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