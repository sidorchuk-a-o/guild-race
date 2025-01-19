using Game.Inventory;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Craft
{
    [Serializable]
    public class ReagentsParams
    {
        [SerializeField] private List<ReagentItemData> items;
        [SerializeField] private GridParamsForItems gridParams = new();

        public IReadOnlyList<ReagentItemData> Items => items;
        public GridParamsForItems GridParams => gridParams;
    }
}