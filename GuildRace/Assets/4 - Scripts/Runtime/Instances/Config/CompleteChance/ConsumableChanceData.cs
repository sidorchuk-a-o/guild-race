using System;
using Game.Inventory;
using UnityEngine;

namespace Game.Instances
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