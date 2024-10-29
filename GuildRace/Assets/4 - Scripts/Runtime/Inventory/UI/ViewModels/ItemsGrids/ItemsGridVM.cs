using AD.Services.Router;
using AD.ToolsCollection;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventory
{
    public class ItemsGridVM : ViewModel, IPlacementContainerVM
    {
        private readonly ItemsGridInfo info;

        private readonly InventoryVMFactory inventoryVMF;

        public string Id { get; }
        public ItemsGridCellType CellType { get; }

        public BoundsInt Bounds { get; }
        public ItemsVM Items { get; }

        public UIStateVM PlacementState { get; }

        public ItemsGridVM(ItemsGridInfo info, InventoryVMFactory inventoryVMF)
        {
            this.info = info;
            this.inventoryVMF = inventoryVMF;

            Id = info.Id;
            CellType = info.CellType;

            Bounds = new(Vector3Int.zero, info.Size);
            Items = new(info.Items, inventoryVMF);

            PlacementState = new();
        }

        protected override void InitSubscribes(CompositeDisp disp)
        {
            Items.AddTo(disp);
            PlacementState.AddTo(disp);
        }

        public ItemVM GetItem(in Vector3Int positionOnGrid)
        {
            return Items.ElementAtOrDefault(positionOnGrid);
        }

        // == Grid ==

        public bool CheckPossibilityOfPlacement(ItemVM itemVM, in Vector3Int positionOnGrid)
        {
            return inventoryVMF.CheckPossibilityOfPlacement(new PlaceInGridArgs
            {
                ItemId = itemVM.Id,
                GridId = info.Id,
                PositionOnGrid = positionOnGrid
            });
        }

        public bool TryPlaceItem(ItemVM itemVM, in Vector3Int positionOnGrid)
        {
            return inventoryVMF.TryPlaceItem(new PlaceInGridArgs
            {
                ItemId = itemVM.Id,
                GridId = info.Id,
                PositionOnGrid = positionOnGrid
            });
        }

        // == IPlacementContainerVM ==

        public IEnumerable<IPlacementContainerVM> GetPlacementsInChildren()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                if (Items[i] is IPlacementContainerVM placement)
                {
                    yield return placement;
                }
            }

            yield break;
        }

        public bool CheckPossibilityOfPlacement(ItemVM itemVM)
        {
            return inventoryVMF.CheckPossibilityOfPlacement(new PlaceInPlacementArgs
            {
                ItemId = itemVM.Id,
                PlacementId = info.Id
            });
        }

        public bool TryPlaceItem(ItemVM itemVM)
        {
            return inventoryVMF.TryPlaceItem(new PlaceInPlacementArgs
            {
                ItemId = itemVM.Id,
                PlacementId = info.Id
            });
        }

        public bool TryRemoveItem(ItemVM itemVM)
        {
            return inventoryVMF.TryRemoveItem(new RemoveFromPlacementArgs
            {
                ItemId = itemVM.Id,
                PlacementId = info.Id
            });
        }
    }
}