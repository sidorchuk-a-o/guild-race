using AD.Services;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;

namespace Game.Inventory
{
    public class InventoryService : Service, IInventoryService
    {
        private readonly InventoryState state;
        private readonly InventoryFactory factory;

        public IInventoryFactory Factory => factory;

        public InventoryService(InventoryConfig config, IObjectResolver resolver)
        {
            state = new(config, resolver);
            factory = new(state, config, resolver);
        }

        public override async UniTask<bool> Init()
        {
            state.Init();

            return await Inited();
        }

        // == Info ==

        public ItemInfo GetItem(string id)
        {
            return state.GetItem(id);
        }

        public ItemSlotInfo GetSlot(string id)
        {
            return state.GetSlot(id);
        }

        public ItemsGridInfo GetGrid(string id)
        {
            return state.GetGrid(id);
        }

        public IPlacementContainer GetPlacement(string id)
        {
            var grid = GetGrid(id);

            if (grid is IPlacementContainer gridPlacement)
            {
                return gridPlacement;
            }

            var item = GetItem(id);

            if (item is IPlacementContainer itemContainer)
            {
                return itemContainer;
            }

            return null;
        }

        // == Grid ==

        public bool CheckPossibilityOfPlacement(PlaceInGridArgs placeArgs)
        {
            var item = GetItem(placeArgs.ItemId);
            var grid = GetGrid(placeArgs.GridId);

            if (item.IsNotExist() || grid.IsNotExist())
            {
                return false;
            }

            var positionOnGrid = placeArgs.PositionOnGrid;

            var result = grid.CheckPossibilityOfPlacement(item, positionOnGrid);

            return result;
        }

        public bool TryPlaceItem(PlaceInGridArgs placeArgs)
        {
            var item = GetItem(placeArgs.ItemId);
            var grid = GetGrid(placeArgs.GridId);

            if (item.IsNotExist() || grid.IsNotExist())
            {
                return false;
            }

            var positionOnGrid = placeArgs.PositionOnGrid;

            var result = grid.TryPlaceItem(item, positionOnGrid);

            return result;
        }

        // == Placement ==

        public bool CheckPossibilityOfPlacement(PlaceInPlacementArgs placeArgs)
        {
            var item = GetItem(placeArgs.ItemId);
            var placement = GetPlacement(placeArgs.PlacementId);

            if (item.IsNotExist() || placement.IsNotExist())
            {
                return false;
            }

            if (placement.CheckBasePlacementParams(item) == false)
            {
                return false;
            }

            if (item is IStackableItem stackableItem)
            {
                var itemStack = stackableItem.Stack.Value;

                foreach (var targetItem in placement.GetItems())
                {
                    if (item.DataId != targetItem.DataId)
                    {
                        continue;
                    }

                    var targetStackableItem = targetItem as IStackableItem;
                    var targetItemStack = targetStackableItem.Stack;

                    if (targetItemStack.IsFulled)
                    {
                        continue;
                    }

                    var availableSpace = targetItemStack.AvailableSpace;

                    if (availableSpace >= itemStack)
                    {
                        return true;
                    }

                    itemStack -= availableSpace;
                }
            }

            return placement.CheckPossibilityOfPlacement(item);
        }

        public bool TryPlaceItem(PlaceInPlacementArgs placeArgs)
        {
            var item = GetItem(placeArgs.ItemId);
            var placement = GetPlacement(placeArgs.PlacementId);

            if (item.IsNotExist() || placement.IsNotExist())
            {
                return false;
            }

            if (placement.CheckBasePlacementParams(item) == false)
            {
                return false;
            }

            if (item is IStackableItem stackableItem)
            {
                foreach (var targetItem in placement.GetItems())
                {
                    if (item.DataId != targetItem.DataId)
                    {
                        continue;
                    }

                    var targetStackableItem = targetItem as IStackableItem;
                    var targetItemStack = targetStackableItem.Stack;

                    if (targetItemStack.IsFulled)
                    {
                        continue;
                    }

                    var currentStackValue = stackableItem.Stack.Value;
                    var availableSpace = targetItemStack.AvailableSpace;

                    var transferCount = Mathf.Min(currentStackValue, availableSpace);

                    TryTransferItem(new TransferItemArgs
                    {
                        SourceItemId = item.Id,
                        TargetItemId = targetItem.Id,
                        Count = transferCount
                    });

                    if (availableSpace >= currentStackValue)
                    {
                        return true;
                    }
                }
            }

            var result = placement.TryPlaceItem(item);

            return result;
        }

        public bool TryRemoveItem(RemoveFromPlacementArgs removeArgs)
        {
            var item = GetItem(removeArgs.ItemId);
            var placement = GetPlacement(removeArgs.PlacementId);

            if (item == null || placement.IsNotExist())
            {
                return false;
            }

            var result = placement.TryRemoveItem(item);

            return result;
        }

        public bool TryTransferItem(TransferBetweenPlacementsArgs transferArgs)
        {
            var item = GetItem(transferArgs.ItemId);

            var sourcePlacement = GetPlacement(transferArgs.SourcePlacementId);
            var targetPlacement = GetPlacement(transferArgs.TargetPlacementId);

            if (item.IsNotExist() || sourcePlacement.IsNotExist() || targetPlacement.IsNotExist())
            {
                return false;
            }

            var placeResult = TryPlaceItem(new PlaceInPlacementArgs
            {
                ItemId = item.Id,
                PlacementId = transferArgs.TargetPlacementId
            });

            if (!placeResult)
            {
                return false;
            }

            var removeResult = TryRemoveItem(new RemoveFromPlacementArgs
            {
                ItemId = item.Id,
                PlacementId = transferArgs.SourcePlacementId
            });

            return removeResult;
        }

        // == Slots ==

        public bool CheckPossibilityOfPlacement(PlaceInSlotArgs equipArgs)
        {
            var item = GetItem(equipArgs.ItemId);
            var slot = GetSlot(equipArgs.SlotId);

            if (item.IsNotExist() || slot.IsNotExist())
            {
                return false;
            }

            return slot.CheckPossibilityOfPlacement(item);
        }

        public bool TryAddItem(PlaceInSlotArgs equipArgs)
        {
            var item = GetItem(equipArgs.ItemId);
            var slot = GetSlot(equipArgs.SlotId);

            if (item.IsNotExist() || slot == null)
            {
                return false;
            }

            var result = slot.TryAddItem(item);

            return result;
        }

        public bool TryRemoveItem(RemoveFromSlotArgs removeArgs)
        {
            var item = GetItem(removeArgs.ItemId);

            if (item == null || !item.IsPlacedInSlot)
            {
                return false;
            }

            var slot = GetSlot(item.SlotId);

            var result = slot.TryRemoveItem();

            return result;
        }

        // == Split / Transfer ==

        public bool CheckPossibilityOfTransfer(TransferItemArgs transferArgs)
        {
            var selectedItem = GetItem(transferArgs.SourceItemId);
            var targetItem = GetItem(transferArgs.TargetItemId);

            if (selectedItem is not IStackableItem ||
                targetItem is not IStackableItem targetStackable)
            {
                return false;
            }

            return targetStackable.CheckPossibilityOfTransfer(selectedItem);
        }

        public bool TryTransferItem(TransferItemArgs transferArgs)
        {
            var selectedItem = GetItem(transferArgs.SourceItemId);
            var targetItem = GetItem(transferArgs.TargetItemId);

            if (selectedItem is not IStackableItem selectedStackable ||
                targetItem is not IStackableItem targetStackable)
            {
                return false;
            }

            var transferCount = transferArgs.Count;

            if (transferCount == selectedStackable.Stack.Value)
            {
                state.RemoveItem(selectedItem.Id);
            }

            selectedStackable.Stack.AddValue(-transferCount);
            targetStackable.Stack.AddValue(transferCount);

            return true;
        }

        public bool TrySplitItem(SplittingItemArgs splittingArgs, out ItemInfo newItem)
        {
            var selectedItem = GetItem(splittingArgs.SelectedItemId);
            var grid = GetGrid(splittingArgs.GridId);

            if (selectedItem is not IStackableItem selectedStackable || grid.IsNotExist())
            {
                newItem = null;
                return false;
            }

            var copyItem = factory.CreateItem(selectedItem.DataId);
            var copyStackable = copyItem as IStackableItem;

            var positionOnGrid = splittingArgs.PositionOnGrid;
            var splittingCount = splittingArgs.Count;

            if (splittingArgs.IsRotated)
            {
                copyItem.Bounds.Rotate();
            }

            copyStackable.Stack.SetValue(splittingCount);

            var placeResult = TryPlaceItem(new PlaceInGridArgs
            {
                ItemId = copyItem.Id,
                GridId = grid.Id,
                PositionOnGrid = positionOnGrid
            });

            if (placeResult)
            {
                selectedStackable.Stack.AddValue(-splittingCount);
            }
            else
            {
                state.RemoveItem(copyItem.Id);
            }

            newItem = copyItem;

            return placeResult;
        }

        // == Take ==

        public bool CheckPossibilityOfTake(TakeItemsArgs takeArgs)
        {
            var grid = GetGrid(takeArgs.GridId);

            if (grid.IsNotExist())
            {
                return false;
            }

            var itemsCount = grid.Items
                .Where(x => x.DataId == takeArgs.ItemDataId)
                .Sum(GetItemCount);

            return itemsCount >= takeArgs.Count;
        }

        public bool TryTakeItems(TakeItemsArgs takeArgs, out List<ItemInfo> itemsTaken)
        {
            var grid = GetGrid(takeArgs.GridId);

            if (grid.IsNotExist())
            {
                itemsTaken = null;
                return false;
            }

            itemsTaken = new();

            var needCount = takeArgs.Count;
            var itemCandidates = grid.Items
                .Where(x => x.DataId == takeArgs.ItemDataId)
                .ToListPool();

            foreach (var itemCandidate in itemCandidates)
            {
                var itemCount = GetItemCount(itemCandidate);

                if (itemCount < needCount)
                {
                    needCount -= itemCount;

                    itemsTaken.Add(itemCandidate);

                    TryRemoveItem(new RemoveFromPlacementArgs
                    {
                        ItemId = itemCandidate.Id,
                        PlacementId = grid.Id
                    });
                }
                else
                {
                    if (itemCandidate is IStackableItem)
                    {
                        var copyItem = factory.CreateItem(itemCandidate.DataId);

                        var copyStackable = copyItem as IStackableItem;
                        var candidateStackable = itemCandidate as IStackableItem;

                        copyStackable.Stack.SetValue(needCount);
                        candidateStackable.Stack.AddValue(-needCount);

                        itemsTaken.Add(copyItem);
                    }
                    else
                    {
                        itemsTaken.Add(itemCandidate);

                        TryRemoveItem(new RemoveFromPlacementArgs
                        {
                            ItemId = itemCandidate.Id,
                            PlacementId = grid.Id
                        });
                    }

                    break;
                }
            }

            itemCandidates.ReleaseListPool();

            return true;
        }

        private static int GetItemCount(ItemInfo item)
        {
            const int defaultCount = 1;

            return item switch
            {
                IStackableItem sItem => sItem.Stack.Value,
                _ => defaultCount
            };
        }

        // == Discard ==

        public bool TryDiscardItem(DiscardItemArgs discardArgs)
        {
            var removeResult = false;

            if (discardArgs.SlotId.IsValid())
            {
                removeResult = TryRemoveItem(new RemoveFromSlotArgs
                {
                    ItemId = discardArgs.ItemId
                });
            }

            if (discardArgs.PlacementId.IsValid())
            {
                removeResult = TryRemoveItem(new RemoveFromPlacementArgs
                {
                    ItemId = discardArgs.ItemId,
                    PlacementId = discardArgs.PlacementId
                });
            }

            return removeResult;
        }
    }
}