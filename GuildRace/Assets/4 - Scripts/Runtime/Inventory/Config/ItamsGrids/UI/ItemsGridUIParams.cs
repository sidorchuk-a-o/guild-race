using System.Collections.Generic;
using System.Linq;
using AD.ToolsCollection;
using UnityEngine;

namespace Game.Inventory
{
    [CreateAssetMenu(fileName = "ItemsGridUIParams", menuName = "Inventory/UI/Items Grid Params", order = 101)]
    public class ItemsGridUIParams : ScriptableData
    {
        [SerializeField] private int cellSize = 65;
        [SerializeField] private List<ItemGridUIParams> parameters;

        private Dictionary<ItemType, ItemGridUIParams> paramsCache;

        public int CellSize => cellSize;

        public ItemGridUIParams GetParams(ItemType itemType)
        {
            paramsCache ??= parameters.ToDictionary(x => x.ItemType, x => x);
            paramsCache.TryGetValue(itemType, out var data);

            return data;
        }
    }
}