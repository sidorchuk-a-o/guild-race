using AD.Services.Router;
using AD.ToolsCollection;
using Game.Input;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Inventory
{
    public class InventoryHighlightController : MonoBehaviour
    {
        private const string highlightedState = "highlighted";
        private const string placeState = "default";
        private const string splitState = "split-or-transfer";
        private const string errorState = "error";

        [Header("Highlighter")]
        [SerializeField] private Highlighter highlighter;

        [Header("Draggable")]
        [SerializeField] private InventoryDraggableController draggableController;

        private IInventoryInputModule inventoryInputs;

        private ItemsGridContainer selectedGrid;
        private ItemSlotContainer selectedSlot;

        private ItemVM highlightedItem;
        private PositionOnGrid positionOnGrid;

        private PickupResult pickupResult;
        private ItemVM selectedItem;

        [Inject]
        public void Inject(IInputService inputService)
        {
            inventoryInputs = inputService.InventoryModule;
        }

        public void Init(CompositeDisp disp)
        {
            inventoryInputs.SplittingModeOn
                .Subscribe(SplittingModeChangedCallback)
                .AddTo(disp);

            ItemsGridContainer.OnInteracted
                .Subscribe(ItemsGridInteractedCallback)
                .AddTo(disp);

            ItemSlotContainer.OnInteracted
                .Subscribe(ItemSlotInteractedCallback)
                .AddTo(disp);

            draggableController.OnPickupItem
                .Subscribe(PickupItemCallback)
                .AddTo(disp);

            draggableController.OnReleaseItem
                .Subscribe(ReleaseItemCallback)
                .AddTo(disp);
        }

        // == Split Item Callbacks ==

        private void SplittingModeChangedCallback(bool modeOn)
        {
            UpdateHighlightCallback();
        }

        // == Drag Callbacks ==

        private void PickupItemCallback(PickupResult result)
        {
            pickupResult = result;
            selectedItem = result.SelectedItem;

            UpdateHighlightCallback();
        }

        private void ReleaseItemCallback(ReleaseResult _)
        {
            pickupResult = null;
            selectedItem = null;

            UpdateHighlightCallback();
        }

        private void ItemsGridInteractedCallback(ItemsGridContainer grid)
        {
            selectedGrid = grid;

            UpdateHighlighterParent();
            UpdateHighlightCallback();
        }

        private void ItemSlotInteractedCallback(ItemSlotContainer slot)
        {
            selectedSlot = slot;

            UpdateHighlightCallback();
        }

        // == Update ==

        private void Update()
        {
            if (selectedGrid == null)
            {
                return;
            }

            var cursorPosition = inventoryInputs.CursorPosition;
            var gridTransform = selectedGrid.transform as RectTransform;

            var positionOnGrid = RectUtils.GetPositionOnGrid(
                cursorPosition: cursorPosition,
                gridTransform: gridTransform,
                selectedItem: selectedItem);

            UpdateHighlight(positionOnGrid);
        }

        private void UpdateHighlighterParent()
        {
            if (selectedGrid != null)
            {
                highlighter.SetParent(selectedGrid.HighlightArea);
            }
        }

        private void UpdateHighlightCallback()
        {
            UpdateHighlight(positionOnGrid);
        }

        private void UpdateHighlight(in PositionOnGrid positionOnGrid)
        {
            this.positionOnGrid = positionOnGrid;

            UpdateHighlighter(positionOnGrid);
            UpdateHighlightedItem(positionOnGrid);
        }

        private void UpdateHighlighter(in PositionOnGrid positionOnGrid)
        {
            if (selectedGrid == null || selectedItem == null)
            {
                highlighter.Show(false);
                return;
            }

            highlighter.SetState(GetHighlighterState(positionOnGrid));
            highlighter.SetBounds(new(positionOnGrid.Item, selectedItem.BoundsVM.Size));

            highlighter.Show(true);
        }

        private string GetHighlighterState(in PositionOnGrid positionOnGrid)
        {
            if (!inventoryInputs.SplittingModeOn.Value)
            {
                if (SelectedItemCanBePlaced(positionOnGrid) ||
                    SelectedItemCanBeInsidePlacement(positionOnGrid))
                {
                    return placeState;
                }
            }

            if (SelectedItemCanBeSplitted(positionOnGrid) ||
                SelectedItemCanBeTransferInHovered(positionOnGrid))
            {
                return splitState;
            }

            return errorState;
        }

        private bool SelectedItemCanBeSplitted(in PositionOnGrid positionOnGrid)
        {
            return SelectedItemCanBePlaced(positionOnGrid)
                && !pickupResult.ThisPositionIsPositionOfSelectedItem(positionOnGrid, selectedGrid)
                && selectedItem is IStackableItemVM stackableItem
                && stackableItem.CheckPossibilityOfSplit()
                && selectedGrid.ViewModel.GetItem(positionOnGrid.Cursor) is null;
        }

        private bool SelectedItemCanBeTransferInHovered(in PositionOnGrid positionOnGrid)
        {
            var hoveredItem = selectedGrid.ViewModel.GetItem(positionOnGrid.Cursor);

            return !pickupResult.ThisPositionIsPositionOfSelectedItem(positionOnGrid, selectedGrid)
                && hoveredItem is IStackableItemVM stackableItem
                && stackableItem.CheckPossibilityOfTransfer(selectedItem);
        }

        private bool SelectedItemCanBePlaced(in PositionOnGrid positionOnGrid)
        {
            return selectedGrid.ViewModel.CheckPossibilityOfPlacement(selectedItem, positionOnGrid.Item);
        }

        private bool SelectedItemCanBeInsidePlacement(in PositionOnGrid positionOnGrid)
        {
            var hoveredItem = selectedGrid.ViewModel.GetItem(positionOnGrid.Cursor);

            return hoveredItem is IPlacementContainerVM placement
                && placement.CheckPossibilityOfPlacement(selectedItem);
        }

        private void UpdateHighlightedItem(in PositionOnGrid positionOnGrid)
        {
            var gridSelected = selectedGrid != null;
            var equipSlotSelected = selectedSlot != null;

            if (gridSelected || equipSlotSelected)
            {
                if (gridSelected)
                {
                    var newHighlightedItem = selectedGrid.ViewModel?.GetItem(positionOnGrid.Cursor);

                    if (newHighlightedItem != highlightedItem)
                    {
                        highlightedItem?.HighlightStateVM.ResetState();
                        highlightedItem = newHighlightedItem;
                        highlightedItem?.HighlightStateVM.SetState(highlightedState);
                    }
                }

                if (equipSlotSelected)
                {
                    if (highlightedItem == null)
                    {
                        highlightedItem?.HighlightStateVM.ResetState();
                        highlightedItem = selectedSlot.ViewModel?.ItemVM.Value;
                        highlightedItem?.HighlightStateVM.SetState(highlightedState);
                    }
                }
            }
            else if (highlightedItem != null)
            {
                highlightedItem?.HighlightStateVM.ResetState();
                highlightedItem = null;
            }
        }
    }
}