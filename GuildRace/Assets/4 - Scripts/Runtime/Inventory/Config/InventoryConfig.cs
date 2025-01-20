using AD.ToolsCollection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Inventory
{
    public class InventoryConfig : ScriptableConfig
    {
        // Items
        [SerializeField] private EquipsParams equipsParams = new();
        // Database
        [SerializeField] private List<ItemData> items = new();
        [SerializeField] private List<ItemSlotData> itemSlots = new();
        [SerializeField] private List<ItemsGridData> itemsGrids = new();
        // Params
        [SerializeField] private ItemsParams itemsParams = new();
        [SerializeField] private ItemSlotsParams itemSlotsParams = new();
        [SerializeField] private ItemsGridsParams itemsGridsParams = new();
        // UI
        [SerializeField] private UIParams uiParams = new();

        private Dictionary<int, ItemData> itemsCache;
        private Dictionary<int, ItemSlotData> itemSlotsCache;
        private Dictionary<int, ItemsGridData> itemsGridsCache;

        public EquipsParams EquipsParams => equipsParams;

        public ItemsParams ItemsParams => itemsParams;
        public ItemSlotsParams ItemSlotsParams => itemSlotsParams;
        public ItemsGridsParams ItemsGridParams => itemsGridsParams;

        public UIParams UIParams => uiParams;

        private void OnEnable()
        {
            uiParams.OnEnable();
        }

        public ItemData GetItem(int id)
        {
            itemsCache ??= items.ToDictionary(x => x.Id, x => x);
            itemsCache.TryGetValue(id, out var data);

            return data;
        }

        public ItemSlotData GetSlot(int id)
        {
            itemSlotsCache ??= itemSlots.ToDictionary(x => x.Id, x => x);
            itemSlotsCache.TryGetValue(id, out var data);

            return data;
        }

        public ItemsGridData GetGrid(int id)
        {
            itemsGridsCache ??= itemsGrids.ToDictionary(x => x.Id, x => x);
            itemsGridsCache.TryGetValue(id, out var data);

            return data;
        }
    }
}