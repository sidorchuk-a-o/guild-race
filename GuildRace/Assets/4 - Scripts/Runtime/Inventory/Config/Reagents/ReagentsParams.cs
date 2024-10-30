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
        [SerializeField] private List<ReagentSlotData> slots;
        // Params
        [SerializeField] private GridParamsForItems gridParams = new();

        public IReadOnlyList<ReagentItemData> Items => items;
        public IReadOnlyList<ReagentSlotData> Slots => slots;

        public GridParamsForItems GridParams => gridParams;
    }
}