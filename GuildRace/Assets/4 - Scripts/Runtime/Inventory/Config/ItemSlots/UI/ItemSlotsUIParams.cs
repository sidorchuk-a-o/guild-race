using System.Collections.Generic;
using System.Linq;
using AD.ToolsCollection;
using UnityEngine;

namespace Game.Inventory
{
    [CreateAssetMenu(fileName = "ItemSlotUIParams", menuName = "Inventory/UI/Item Slot Params", order = 100)]
    public class ItemSlotsUIParams : ScriptableData
    {
        [SerializeField] private List<ItemSlotUIParams> slotsParams;

        private Dictionary<ItemType, ItemSlotUIParams> paramsCache;

        public ItemSlotUIParams GetParams(ItemType itemType)
        {
            paramsCache ??= slotsParams.ToDictionary(x => x.ItemType, x => x);
            paramsCache.TryGetValue(itemType, out var data);

            return data;
        }
    }
}