using System;
using AD.Game.Inventory;
using AD.Store;
using UnityEngine;

namespace AD.Game.Craft
{
    [Serializable]
    public class RecyclingReagentData
    {
        [SerializeField] private Rarity rarity;
        [SerializeField] private CurrencyKey currencyKey;
        [SerializeField] private int amount;

        public Rarity Rarity => rarity;
        public CurrencyKey CurrencyKey => currencyKey;
        public int Amount => amount;
    }
}