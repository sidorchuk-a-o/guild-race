using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventory
{
    [Serializable]
    public class ReagentsParams
    {
        // Items
        [SerializeField] private List<ReagentItemData> items;
        // Params
        [SerializeField] private SlotParamsForItems slotParams = new();
        [SerializeField] private GridParamsForItems gridParams = new();

        public IReadOnlyList<ReagentItemData> Items => items;

        public SlotParamsForItems SlotParams => slotParams;
        public GridParamsForItems GridParams => gridParams;
    }
}