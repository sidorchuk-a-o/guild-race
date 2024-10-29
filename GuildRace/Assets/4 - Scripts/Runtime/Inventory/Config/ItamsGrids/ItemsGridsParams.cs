using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventory
{
    [Serializable]
    public class ItemsGridsParams
    {
        [SerializeField] private List<ItemsGridCategoryData> categories;
        [SerializeField] private List<ItemsGridCellTypeData> cellTypes;
    }
}