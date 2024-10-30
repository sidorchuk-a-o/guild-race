using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventory
{
    [Serializable]
    public class ItemSlotsParams
    {
        [SerializeField] private List<ItemSlotsFactory> factories;

        public IReadOnlyList<ItemSlotsFactory> Factories => factories;
    }
}