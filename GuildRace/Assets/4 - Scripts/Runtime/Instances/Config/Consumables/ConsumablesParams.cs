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
        [SerializeField] private List<ConsumableTypeData> types;
        [SerializeField] private List<ConsumableMechanicHandler> mechanicHandlers;
        [SerializeField] private GridParamsForItems gridParams = new();

        public IReadOnlyList<ConsumablesItemData> Items => items;
        public IReadOnlyList<ConsumableTypeData> Types => types;
        public IReadOnlyList<ConsumableMechanicHandler> MechanicHandlers => mechanicHandlers;
        public GridParamsForItems GridParams => gridParams;
    }
}