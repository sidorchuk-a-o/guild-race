using Game.Inventory;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances
{
    [Serializable]
    public class ConsumablesParams
    {
        [SerializeField] private List<ConsumablesItemData> items;
        [SerializeField] private GridParamsForItems gridParams = new();

        public IReadOnlyList<ConsumablesItemData> Items => items;
        public GridParamsForItems GridParams => gridParams;
    }
}