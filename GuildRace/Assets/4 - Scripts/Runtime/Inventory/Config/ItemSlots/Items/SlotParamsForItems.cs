using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventory
{
    [Serializable]
    public class SlotParamsForItems
    {
        [SerializeField] private List<ItemSlot> slots;

        public IReadOnlyList<ItemSlot> Slots => slots;
    }
}