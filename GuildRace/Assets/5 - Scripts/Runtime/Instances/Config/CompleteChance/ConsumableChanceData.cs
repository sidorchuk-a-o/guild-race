using System;
using AD.Game.Inventory;
using UnityEngine;

namespace AD.Game.Instances
{
    [Serializable]
    public class ConsumableChanceData
    {
        [SerializeField] private Rarity rarity;
        [SerializeField] private float chanceMod;

        public Rarity Rarity => rarity;
        public float ChanceMod => chanceMod;
    }
}