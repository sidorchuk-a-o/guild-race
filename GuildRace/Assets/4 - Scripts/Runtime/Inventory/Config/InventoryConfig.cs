﻿using AD.ToolsCollection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Inventory
{
    public class InventoryConfig : ScriptableConfig
    {
        // Equips
        [SerializeField] private EquipsParams equipsParams = new();
        [SerializeField] private ReagentsParams reagentsParams = new();
        // Database
        [SerializeField] private List<ItemData> items = new();
        [SerializeField] private List<ItemsGridData> itemsGrids = new();
        // Params
        [SerializeField] private ItemsParams itemsParams = new();
        [SerializeField] private ItemSlotsParams itemSlotsParams = new();
        [SerializeField] private ItemsGridsParams itemsGridsParams = new();
        // UI
        [SerializeField] private UIParams uiParams = new();

        private Dictionary<string, ItemData> itemsCache;
        private Dictionary<string, ItemsGridData> itemsGridsCache;

        public EquipsParams EquipsParams => equipsParams;
        public ReagentsParams ReagentsParams => reagentsParams;

        public ItemsParams ItemsParams => itemsParams;
        public ItemSlotsParams ItemSlotsParams => itemSlotsParams;
        public ItemsGridsParams ItemsGridParams => itemsGridsParams;

        public UIParams UIParams => uiParams;

        private void OnEnable()
        {
            uiParams.OnEnable();
        }

        public ItemData GetItem(string id)
        {
            itemsCache ??= items.ToDictionary(x => x.Id, x => x);

            return itemsCache.TryGetValue(id, out var data) ? data : null;
        }

        public ItemsGridData GetGrid(string id)
        {
            itemsGridsCache ??= itemsGrids.ToDictionary(x => x.Id, x => x);

            return itemsGridsCache.TryGetValue(id, out var data) ? data : null;
        }
    }
}