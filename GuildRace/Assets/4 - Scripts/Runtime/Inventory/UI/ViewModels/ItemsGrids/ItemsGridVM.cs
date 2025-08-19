using Game.UI;
using AD.Services.Router;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Game.Inventory
{
    public class ItemsGridVM : ViewModel, IPlacementContainerVM
    {
        private readonly ItemsGridInfo info;
        private readonly ReactiveProperty<BoundsInt> bounds = new();

        private readonly InventoryVMFactory inventoryVMF;

        public string Id { get; }

        public ItemsGridCategory Category { get; }
        public ItemsGridCellType CellType { get; }
        public IReadOnlyReactiveProperty<BoundsInt> Bounds => bounds;

        public ItemsVM ItemsVM { get; }
        public UIStateVM PlacementStateVM { get; }

        public ItemsGridVM(ItemsGridInfo info, InventoryVMFactory inventoryVMF)
        {
            this.info = info;
            this.inventoryVMF = inventoryVMF;

            Id = info.Id;
            Category = info.Category;
            CellType = info.CellType;
            bounds.Value = new(Vector3Int.zero, info.Size.Value);

            ItemsVM = new(info.Items, inventoryVMF);
            PlacementStateVM = new();
        }

        protected override void InitSubscribes()
        {
            info.Size
                .Subscribe(x => bounds.Value = new(Vector3Int.zero, x))
                .AddTo(this);

            ItemsVM.AddTo(this);
            PlacementStateVM.AddTo(this);
        }

        public ItemVM GetItem(in Vector3Int positionOnGrid)
        {
            return ItemsVM.ElementAtOrDefault(positionOnGrid);
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
            for (var i = 0; i < ItemsVM.Count; i++)
            {
                if (ItemsVM[i] is IPlacementContainerVM placementVM)
                {
                    yield return placementVM;
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