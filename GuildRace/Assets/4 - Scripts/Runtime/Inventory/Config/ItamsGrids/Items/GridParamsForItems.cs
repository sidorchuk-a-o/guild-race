using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventory
{
    [Serializable]
    public class GridParamsForItems
    {
        [SerializeField] private List<ItemsGridCategory> categories;
        [SerializeField] private List<ItemsGridCellType> cellTypes;

        public IReadOnlyList<ItemsGridCategory> Categories => categories;
        public IReadOnlyList<ItemsGridCellType> CellTypes => cellTypes;
    }
}