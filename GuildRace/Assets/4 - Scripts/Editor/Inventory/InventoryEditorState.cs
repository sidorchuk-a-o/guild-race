using AD.ToolsCollection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Callbacks;

namespace Game.Inventory
{
    public class InventoryEditorState : EditorState<InventoryEditorState>
    {
        private InventoryConfig config;
        private InventoryEditorsFactory editorsFactory;
        private ItemVMEditorsFactory itemVMEditorsFactory;
        private PickupHandlerEditorsFactory pickupHandlersFactory;
        private ReleaseHandlerEditorsFactory releaseHandlerFactory;
        private OptionHandlerEditorsFactory optionHandlerFactory;

        private SerializedData itemsCacheData;
        private SerializedData gridsCacheData;

        public static InventoryConfig Config => FindAsset(ref instance.config);

        public static InventoryEditorsFactory EditorsFactory => instance.editorsFactory ??= new();
        public static ItemVMEditorsFactory ItemVMEditorsFactory => instance.itemVMEditorsFactory ??= new();
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

            SaveEditorUtils.OnStartSavePropcess -= UpdateScriptFiles;
            SaveEditorUtils.OnStartSavePropcess += UpdateScriptFiles;
        }

        [DidReloadScripts]
        private static void CheckScriptFiles()
        {
            if (Config == null)
            {
                return;
            }

            KeyScriptFileUtils.CheckScriptFile(GetOptionKeyScriptData());
        }

        private static void UpdateScriptFiles()
        {
            KeyScriptFileUtils.UpdateScriptFile(GetOptionKeyScriptData());
        }

        // == Cache ==

        private void InitItemsCache()
        {
            var config = new SerializedData(Config);

            itemsCacheData = config.GetProperty("items");
            gridsCacheData = config.GetProperty("itemsGrids");
        }

        private void DataFactory_OnDataCreate(object data)
        {
            switch (data)
            {
                case InventoryConfig: InitItemsCache(); break;

                case ItemData: AddToCache(data, itemsCacheData); break;
                case ItemsGridData: AddToCache(data, gridsCacheData); break;
            }
        }

        private void DataFactory_OnDataRemove(object data)
        {
            switch (data)
            {
                case ItemData: RemoveFromCache(data, itemsCacheData); break;
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

        public static Collection<string> GetRaritiesCollection()
        {
            return Config.CreateKeyViewCollection<RarityData>("itemsParams.rarities");
        }

        public static Collection<string> GetItemSlotsCollection()
        {
            return Config.CreateKeyViewCollection<ItemSlotData>("itemSlotsParams.slots");
        }

        public static Collection<string> GetItemsGridCategoriesCollection()
        {
            return Config.CreateKeyViewCollection<ItemsGridCategoryData>("itemsGridsParams.categories");
        }

        public static Collection<string> GetItemsGridCellTypesCollection()
        {
            return Config.CreateKeyViewCollection<ItemsGridCellTypeData>("itemsGridsParams.cellTypes");
        }

        public static Collection<string> GetEquipGroupsCollection()
        {
            return Config.CreateKeyViewCollection<EquipGroupData>("equipsParams.groups");
        }

        public static Collection<string> GetEquipTypesCollection()
        {
            var keysDict = new Dictionary<string, string>();
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

            return new Collection<string>(keysDict.Values, keysDict.Keys, autoSort: false);
        }

        private static KeyScriptData GetOptionKeyScriptData() => new()
        {
            keyTypeName = nameof(OptionKey),
            namespaceValue = "Game.Inventory",
            getCollection = CreateOptionsCollection
        };

        public static Collection<string> CreateOptionsCollection()
        {
            return Config.CreateKeyCollection<OptionHandler>("uiParams.optionHandlers");
        }

        public static Collection<string> CreateOptionsViewCollection()
        {
            return Config.CreateKeyViewCollection<OptionHandler>("uiParams.optionHandlers");
        }

        public static IReadOnlyList<Type> GetExistOptionHandlerTypes()
        {
            return Config
                .GetValue<List<OptionHandler>>("uiParams.optionHandlers")
                .Select(x => x.GetType())
                .ToList();
        }
    }
}