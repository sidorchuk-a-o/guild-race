using Game.Inventory;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Instances
{
    [Serializable]
    public class СonsumablesParams
    {
        [SerializeField] private List<СonsumablesItemData> items;
        [SerializeField] private GridParamsForItems gridParams = new();

        public IReadOnlyList<СonsumablesItemData> Items => items;
        public GridParamsForItems GridParams => gridParams;
    }
}