using AD.Services.Store;
using Game.Inventory;
using System;
using UnityEngine;

namespace Game.Craft
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