using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventory
{
    [Serializable]
    public class СonsumablesParams
    {
        // Items
        [SerializeField] private List<СonsumablesItemData> items;
        [SerializeField] private List<СonsumablesSlotData> slots;
        // Params
        [SerializeField] private GridParamsForItems gridParams = new();

        public IReadOnlyList<СonsumablesItemData> Items => items;
        public IReadOnlyList<СonsumablesSlotData> Slots => slots;

        public GridParamsForItems GridParams => gridParams;
    }
}