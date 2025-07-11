﻿
using System.Collections.Generic;

namespace Game.Inventory
{
    public interface IInventoryService
    {
        IInventoryFactory Factory { get; }

        IReadOnlyCollection<ItemInfo> Items { get; }
        IReadOnlyCollection<ItemSlotInfo> ItemSlots { get; }
        IReadOnlyCollection<ItemsGridInfo> ItemsGrids { get; }

        ItemInfo GetItem(string id);
        ItemSlotInfo GetSlot(string id);
        ItemsGridInfo GetGrid(string id);

        bool CheckPossibilityOfPlacement(PlaceInGridArgs placeArgs);
        bool TryPlaceItem(PlaceInGridArgs placeArgs);

        bool CheckPossibilityOfPlacement(PlaceInPlacementArgs placeArgs);
        bool TryPlaceItem(PlaceInPlacementArgs placeArgs);
        bool TryRemoveItem(RemoveFromPlacementArgs removeArgs);
        bool TryTransferItem(TransferBetweenPlacementsArgs transferArgs);

        bool CheckPossibilityOfPlacement(PlaceInSlotArgs equipArgs);
        bool TryAddItem(PlaceInSlotArgs equipArgs);
        bool TryRemoveItem(RemoveFromSlotArgs removeArgs);

        bool CheckPossibilityOfTransfer(TransferItemArgs transferItemArgs);
        bool TryTransferItem(TransferItemArgs transferArgs);
        bool TrySplitItem(SplittingItemArgs splittingArgs, out ItemInfo newItem);

        bool CheckPossibilityOfTake(TakeItemsArgs takeArgs);
        bool TryTakeItems(TakeItemsArgs takeArgs, out List<ItemInfo> itemsTaken);

        bool TryDiscardItem(DiscardItemArgs discardArgs);
    }
}