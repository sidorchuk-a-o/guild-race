using System;
using System.Collections.Generic;
using UnityEngine;

namespace AD.Game.Inventory
{
    [Serializable]
    public class ItemsGridsParams
    {
        [SerializeField] private List<ItemsGridCategoryData> categories;
        [SerializeField] private List<ItemsGridCellTypeData> cellTypes;
        [SerializeField] private List<ItemsGridsFactory> factories;

        public IReadOnlyList<ItemsGridsFactory> Factories => factories;
    }
}