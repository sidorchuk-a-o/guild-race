using System.Collections.Generic;
using System.Linq;
using AD.ToolsCollection;
using UnityEngine;

namespace AD.Game.Inventory
{
    [CreateAssetMenu(fileName = "ItemsTooltipUIParams", menuName = "Inventory/UI/Tooltip Params", order = 100)]
    public class ItemsTooltipUIParams : ScriptableData
    {
        [SerializeField] private List<ItemTooltipUIParams> parameters;

        private Dictionary<ItemType, ItemTooltipUIParams> paramsCache;

        public ItemTooltipUIParams GetParams(ItemType itemType)
        {
            paramsCache ??= parameters.ToDictionary(x => x.ItemType, x => x);
            paramsCache.TryGetValue(itemType, out var data);

            return data;
        }
    }
}