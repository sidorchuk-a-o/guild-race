﻿using AD.ToolsCollection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Game.Inventory
{
    public class InventoryEditorState : EditorState<InventoryEditorState>
    {
        private InventoryConfig config;

        private InventoryEditorsFactory editorsFactory;
        private ItemSlotEditorsFactory itemSlotEditorsFactory;
        private ItemsGridEditorsFactory itemGridEditorsFactory;

        private ItemVMEditorsFactory itemVMEditorsFactory;
        private ItemSlotVMEditorsFactory itemSlotVMEditorsFactory;

        private PickupHandlerEditorsFactory pickupHandlersFactory;
        private ReleaseHandlerEditorsFactory releaseHandlerFactory;
        private OptionHandlerEditorsFactory optionHandlerFactory;

        private SerializedData itemsCacheData;
        private SerializedData slotsCacheData;
        private SerializedData gridsCacheData;

        private readonly InventoryScriptGenerator scriptGenerator = new();

        public static InventoryConfig Config => FindAsset(ref instance.config);

        public static InventoryEditorsFactory EditorsFactory => instance.editorsFactory ??= new();
        public static ItemSlotEditorsFactory ItemSlotEditorsFactory => instance.itemSlotEditorsFactory ??= new();
        public static ItemsGridEditorsFactory ItemsGridEditorsFactory => instance.itemGridEditorsFactory ??= new();
        public static ItemVMEditorsFactory ItemVMEditorsFactory => instance.itemVMEditorsFactory ??= new();
        public static ItemSlotVMEditorsFactory ItemSlotVMEditorsFactory => instance.itemSlotVMEditorsFactory ??= new();
        public static PickupHandlerEditorsFactory PickupHandlersFactory => instance.pickupHandlersFactory ??= new();
        public static ReleaseHandlerEditorsFactory ReleaseHandlerFactory => instance.releaseHandlerFactory ??= new();
        public static OptionHandlerEditorsFactory OptionHandlerFactory => instance.optionHandlerFactory ??= new();

        private void OnEnable()
        {
            InitItemsCache();

            DataFactory.OnDataCreate -= DataFactory_OnDataCreate;
            DataFactory.OnDataCreate += DataFactory_OnDataCreate;

            DataFactory.OnDataRemove -= DataFactory_OnDataRemove;
            DataFactory.OnDataRemove += DataFactory_OnDataRemove;
        }

        // == Cache ==

        private void InitItemsCache()
        {
            var config = new SerializedData(Config);

            itemsCacheData = GetCache(config, "items");
            slotsCacheData = GetCache(config, "itemSlots");
            gridsCacheData = GetCache(config, "itemsGrids");
        }

        private static SerializedData GetCache(SerializedData config, string PropName)
        {
            var cache = config.GetProperty(PropName);

            if (cache.GetValue<IList>() is IList list)
            {
                for (var i = list.Count - 1; i >= 0; i--)
                {
                    if (list[i] == null)
                    {
                        list.RemoveAt(i);

                        cache.MarkAsDirty();
                    }
                }
            }

            return cache;
        }

        private void DataFactory_OnDataCreate(object data)
        {
            switch (data)
            {
                case InventoryConfig: InitItemsCache(); break;

                case ItemData: AddToCache(data, itemsCacheData); break;
                case ItemSlotData: AddToCache(data, slotsCacheData); break;
                case ItemsGridData: AddToCache(data, gridsCacheData); break;
            }
        }

        private void DataFactory_OnDataRemove(object data)
        {
            switch (data)
            {
                case ItemData: RemoveFromCache(data, itemsCacheData); break;
                case ItemSlotData: RemoveFromCache(data, slotsCacheData); break;
                case ItemsGridData: RemoveFromCache(data, gridsCacheData); break;
            }
        }

        private static void AddToCache(object item, SerializedData cache)
        {
            cache.GetValue<IList>().Add(item);
            cache.MarkAsDirty();
        }

        private static void RemoveFromCache(object data, SerializedData cache)
        {
            cache.GetValue<IList>().Remove(data);
            cache.MarkAsDirty();
        }

        // == Collections ==

        public static Collection<int> CreateItemTypesCollection()
        {
            return Config.CreateKeyCollection<ItemsFactory, int>("itemsParams.factories");
        }

        public static Collection<int> GetRaritiesCollection()
        {
            return Config.CreateKeyViewCollection<RarityData, int>("itemsParams.rarities");
        }

        public static Collection<int> GetItemSlotsCollection()
        {
            var slots = Config.GetValue<List<ItemSlotData>>("itemSlots");
            var keysDict = new Dictionary<string, int>
            {
                ["<none>"] = -1
            };

            foreach (var slot in slots)
            {
                var slotType = slot.GetType();
                var slotEditorData = ItemSlotEditorsFactory.GetEditorData(slotType);

                keysDict[$"{slotEditorData.Title}/{slot.Title}"] = slot.Id;
            }

            return new(keysDict.Values, keysDict.Keys, autoSort: false);
        }

        public static Collection<int> GetItemsGridCategoriesCollection()
        {
            return Config.CreateKeyViewCollection<ItemsGridCategoryData, int>("itemsGridsParams.categories");
        }

        public static Collection<int> GetItemsGridCellTypesCollection()
        {
            return Config.CreateKeyViewCollection<ItemsGridCellTypeData, int>("itemsGridsParams.cellTypes");
        }

        public static Collection<int> GetEquipGroupsCollection()
        {
            return Config.CreateKeyViewCollection<EquipGroupData, int>("equipsParams.groups");
        }

        public static Collection<int> GetEquipTypesCollection()
        {
            var keysDict = new Dictionary<string, int>();
            var equipGroups = Config.GetValue<List<EquipGroupData>>("equipsParams.groups");

            foreach (var equipGroup in equipGroups)
            {
                var groupKey = equipGroup.Title;

                foreach (var equipType in equipGroup.GetValue<List<EquipTypeData>>("types"))
                {
                    var typeKey = $"{groupKey}/{equipType.Title}";

                    keysDict[typeKey] = equipType.Id;
                }
            }

            return new(keysDict.Values, keysDict.Keys, autoSort: false);
        }

        public static Collection<string> CreateOptionsCollection()
        {
            return Config.CreateKeyCollection<OptionHandler, string>("uiParams.optionHandlers");
        }

        public static Collection<string> CreateOptionsViewCollection()
        {
            return Config.CreateKeyViewCollection<OptionHandler, string>("uiParams.optionHandlers");
        }

        public static IReadOnlyList<Type> GetExistOptionHandlerTypes()
        {
            return Config
                .GetValue<List<OptionHandler>>("uiParams.optionHandlers")
                .Select(x => x.GetType())
                .ToList();
        }

        public static Collection<int> GetAllItemsCollection()
        {
            var items = Config.GetValue<List<ItemData>>("items");
            var itemsGroups = items.GroupBy(x => x.GetType().Name);

            var values = new List<int>();
            var options = new List<string>();

            foreach (var group in itemsGroups)
            {
                var groupName = group.Key.Clear("Item", "Data");

                foreach (var item in group)
                {
                    var option = $"{groupName}/{item.Title}";

                    values.Add(item.Id);
                    options.Add(option);
                }
            }

            return new Collection<int>(values, options, autoSort: false);
        }
    }
}