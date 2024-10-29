using AD.Services.Pools;
using AD.Services.Router;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Inventory
{
    public class InventoryVMFactory : VMFactory
    {
        private readonly IInventoryService inventoryService;

        private readonly GameObject poolsContainer;
        private readonly PoolContainer<Sprite> spritesPool;
        private readonly PoolContainer<GameObject> objectsPool;

        private Dictionary<Type, ItemsVMFactory> itemFactoriesDict;

        public InventoryVMFactory(IPoolsService pools, IInventoryService inventoryService)
        {
            this.inventoryService = inventoryService;

            if (poolsContainer == null)
            {
                poolsContainer = new("--- Inventory Pools ---");
                poolsContainer.DontDestroyOnLoad();

                spritesPool = pools.CreateAssetPool<Sprite>();
                objectsPool = pools.CreatePrefabPool<GameObject>(poolsContainer.transform);
            }
        }

        public void SetItemFactories(IReadOnlyList<ItemsVMFactory> itemVMFactories)
        {
            itemFactoriesDict = itemVMFactories.ToDictionary(x => x.InfoType, x => x);
        }

        // == Items ==

        public ItemVM CreateItem(string itemId)
        {
            var itemInfo = inventoryService.GetItem(itemId);

            return CreateItem(itemInfo);
        }

        public ItemVM CreateItem(ItemInfo info)
        {
            if (info == null)
            {
                return null;
            }

            var factory = itemFactoriesDict[info.GetType()];

            return factory.Create(info, this);
        }

        // == Slots ==

        public ItemSlotVM CreateItemSlot(ItemSlotInfo info)
        {
            return new ItemSlotVM(info, this);
        }

        public ItemSlotsVM CreateItemSlots(IItemSlotsCollection itemSlots)
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
            return inventoryService.TrySplitItem(splittingArgs);
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

        public UniTask<GameObject> RentItemInGridAsync(ItemVM itemVM)
        {
            var type = itemVM.InfoType;
            var factory = itemFactoriesDict[type];

            return objectsPool.RentAsync(factory.ItemInGridRef);
        }

        public UniTask<GameObject> RentItemInSlotAsync(ItemVM itemVM)
        {
            var type = itemVM.InfoType;
            var factory = itemFactoriesDict[type];

            return objectsPool.RentAsync(factory.ItemInSlotRef);
        }

        public UniTask PreloadIconsAsync(AssetReference assetRef)
        {
            return spritesPool.PreloadAsync(assetRef, preloadCount: 1, threshold: 1);
        }

        public UniTask PreloadObjectsAsync(AssetReference assetRef, int preloadCount, int threshold)
        {
            return objectsPool.PreloadAsync(assetRef, preloadCount, threshold);
        }

        public UniTask<GameObject> RentObjectAsync(AssetReference objectRef)
        {
            return objectsPool.RentAsync(objectRef);
        }

        public UniTask<Sprite> RentSprite(AssetReference iconRef)
        {
            return spritesPool.RentAsync(iconRef);
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