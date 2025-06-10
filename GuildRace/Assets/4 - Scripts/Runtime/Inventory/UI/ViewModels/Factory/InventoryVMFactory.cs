using AD.Services.Pools;
using AD.Services.Router;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace Game.Inventory
{
    public class InventoryVMFactory : VMFactory
    {
        private readonly InventoryConfig inventoryConfig;
        private readonly IInventoryService inventoryService;
        private readonly IObjectResolver resolver;

        private readonly GameObject poolsContainer;
        private readonly PoolContainer<Sprite> spritesPool;
        private readonly PoolContainer<GameObject> objectsPool;

        private Dictionary<Type, ItemsVMFactory> itemsFactoriesDict;
        private Dictionary<Type, ItemSlotsVMFactory> slotsFactoriesDict;

        public InventoryVMFactory(
            InventoryConfig inventoryConfig,
            IInventoryService inventoryService,
            IPoolsService pools,
            IObjectResolver resolver)
        {
            this.inventoryConfig = inventoryConfig;
            this.inventoryService = inventoryService;
            this.resolver = resolver;

            if (poolsContainer == null)
            {
                poolsContainer = new("--- Inventory Pools ---");
                poolsContainer.DontDestroyOnLoad();

                spritesPool = pools.CreateAssetPool<Sprite>();
                objectsPool = pools.CreatePrefabPool<GameObject>(poolsContainer.transform);
            }
        }

        public void SetItemsFactories(IReadOnlyList<ItemsVMFactory> itemsFactories)
        {
            itemsFactoriesDict = itemsFactories.ToDictionary(x => x.DataType, x => x);
            itemsFactoriesDict.ForEach(x => resolver.Inject(x.Value));
        }

        public void SetItemSlotsFactories(IReadOnlyList<ItemSlotsVMFactory> slotsFactories)
        {
            slotsFactoriesDict = slotsFactories.ToDictionary(x => x.InfoType, x => x);
            slotsFactoriesDict.ForEach(x => resolver.Inject(x.Value));
        }

        public EquipTypeVM GetEquipType(EquipType type)
        {
            var data = inventoryConfig.EquipsParams.GetType(type);

            return new EquipTypeVM(data);
        }

        public EquipGroupVM GetEquipGroup(EquipGroup group)
        {
            var data = inventoryConfig.EquipsParams.GetGroup(group);

            return new EquipGroupVM(data, this);
        }

        public RarityDataVM GetRarity(Rarity rarity)
        {
            var data = inventoryConfig.ItemsParams.GetRarity(rarity);

            return new RarityDataVM(data);
        }

        // == Items ==

        public ItemDataVM CreateItemData(int dataId)
        {
            var data = inventoryConfig.GetItem(dataId);
            var factory = itemsFactoriesDict[data.GetType()];

            return factory.Create(data, this);
        }

        public ItemVM CreateItem(string itemId)
        {
            var info = inventoryService.GetItem(itemId);

            return CreateItem(info);
        }

        public ItemVM CreateItem(ItemInfo info)
        {
            if (info == null)
            {
                return null;
            }

            var factory = itemsFactoriesDict[info.DataType];

            return factory.Create(info, this);
        }

        public ItemCounterVM CreateItemCounter(int itemDataId, IEnumerable<ItemsGridInfo> itemsGrids)
        {
            return new ItemCounterVM(itemDataId, itemsGrids, this);
        }

        // == Slots ==

        public ItemSlotDataVM GetSlotData(ItemSlot slot)
        {
            var data = inventoryConfig.GetSlot(slot);

            return new ItemSlotDataVM(data);
        }

        public ItemSlotVM CreateSlot(string id)
        {
            var info = inventoryService.GetSlot(id);

            return CreateSlot(info);
        }

        public ItemSlotVM CreateSlot(ItemSlotInfo info)
        {
            if (info == null)
            {
                return null;
            }

            var factory = slotsFactoriesDict[info.GetType()];

            return factory.Create(info, this);
        }

        public ItemSlotsVM CreateSlots(IItemSlotsCollection itemSlots)
        {
            return new ItemSlotsVM(itemSlots, this);
        }

        public bool CheckPossibilityOfPlacement(PlaceInSlotArgs equipArgs)
        {
            return inventoryService.CheckPossibilityOfPlacement(equipArgs);
        }

        public bool TryEquipItem(PlaceInSlotArgs equipArgs)
        {
            return inventoryService.TryAddItem(equipArgs);
        }

        public bool TryRemoveItem(RemoveFromSlotArgs removeArgs)
        {
            return inventoryService.TryRemoveItem(removeArgs);
        }

        // == Grid ==

        public ItemsGridVM CreateItemsGrid(ItemsGridInfo info)
        {
            return new ItemsGridVM(info, this);
        }

        public bool CheckPossibilityOfPlacement(PlaceInGridArgs placeArgs)
        {
            return inventoryService.CheckPossibilityOfPlacement(placeArgs);
        }

        public bool TryPlaceItem(PlaceInGridArgs placeArgs)
        {
            return inventoryService.TryPlaceItem(placeArgs);
        }

        // == Placement ==

        public bool CheckPossibilityOfPlacement(PlaceInPlacementArgs placeArgs)
        {
            return inventoryService.CheckPossibilityOfPlacement(placeArgs);
        }

        public bool TryPlaceItem(PlaceInPlacementArgs placeArgs)
        {
            return inventoryService.TryPlaceItem(placeArgs);
        }

        public bool TryRemoveItem(RemoveFromPlacementArgs removeArgs)
        {
            return inventoryService.TryRemoveItem(removeArgs);
        }

        public bool TryTransferItem(TransferBetweenPlacementsArgs transferArgs)
        {
            return inventoryService.TryTransferItem(transferArgs);
        }

        // == Stackable ==

        public bool CheckPossibilityOfTransfer(TransferItemArgs transferItemArgs)
        {
            return inventoryService.CheckPossibilityOfTransfer(transferItemArgs);
        }

        public bool TrySplitItem(SplittingItemArgs splittingArgs)
        {
            return inventoryService.TrySplitItem(splittingArgs, out _);
        }

        public bool TryTransferItem(TransferItemArgs transferArgs)
        {
            return inventoryService.TryTransferItem(transferArgs);
        }

        // == Discard ==

        public bool TryDiscardItem(DiscardItemArgs discardArgs)
        {
            return inventoryService.TryDiscardItem(discardArgs);
        }

        // == Pools ==

        public UniTask PreloadIconsAsync(AssetReference assetRef)
        {
            return spritesPool.PreloadAsync(assetRef, preloadCount: 1, threshold: 1);
        }

        public UniTask PreloadObjectsAsync(AssetReference assetRef, int preloadCount, int threshold)
        {
            return objectsPool.PreloadAsync(assetRef, preloadCount, threshold);
        }

        public UniTask<GameObject> RentObjectAsync(AssetReference objectRef, CancellationToken token = default)
        {
            return objectsPool.RentAsync(objectRef, token: token);
        }

        public UniTask<Sprite> RentImage(AssetReference iconRef, CancellationToken token = default)
        {
            return spritesPool.RentAsync(iconRef, token: token);
        }

        public void ReturnItems<TComponent>(IEnumerable<TComponent> instances)
            where TComponent : Component
        {
            foreach (var instance in instances)
            {
                ReturnItem(instance);
            }
        }

        public void ReturnItem<TComponent>(TComponent instance)
            where TComponent : Component
        {
            objectsPool.Return(instance.gameObject);
        }
    }
}