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

        private Transform highlighterDefaultParent;

        private IInventoryInputModule inventoryInputs;

        private ItemSlotVM selectedSlotVM;
        private ItemsGridVM selectedGridVM;

        private ItemsGridContainer selectedGridContainer;

        private ItemVM highlightedItemVM;
        private PositionOnGrid positionOnGrid;

        private ItemVM selectedItemVM;
        private PickupResult pickupResult;

        [Inject]
        public void Inject(IInputService inputService)
        {
            inventoryInputs = inputService.InventoryModule;
        }

        private void Awake()
        {
            highlighterDefaultParent = highlighter.transform.parent;
        }

        public void Init(CompositeDisp disp)
        {
            ItemSlotContainer.OnInteracted
                .Subscribe(ItemSlotInteractedCallback)
                .AddTo(disp);

            ItemsGridContainer.OnInteracted
                .Subscribe(ItemsGridInteractedCallback)
                .AddTo(disp);

            inventoryInputs.SplittingModeOn
                .Subscribe(SplittingModeChangedCallback)
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
            UpdateHighlight();
        }

        // == Drag Callbacks ==

        private void PickupItemCallback(PickupResult result)
        {
            pickupResult = result;
            selectedItemVM = result.SelectedItemVM;

            UpdateHighlight();
        }

        private void ReleaseItemCallback(ReleaseResult _)
        {
            pickupResult = null;
            selectedItemVM = null;

            UpdateHighlight();
        }

        private void ItemSlotInteractedCallback(ItemSlotContainer itemSlot)
        {
            selectedSlotVM = itemSlot?.ViewModel;

            UpdateHighlight();
        }

        private void ItemsGridInteractedCallback(ItemsGridContainer itemsGrid)
        {
            selectedGridVM = itemsGrid?.ViewModel;
            selectedGridContainer = itemsGrid;

            UpdateHighlighterParent();
            UpdateHighlight();
        }

        // == Update ==

        private void Update()
        {
            if (selectedGridVM == null)
            {
                return;
            }

            var cursorPosition = inventoryInputs.CursorPosition;
            var gridTransform = selectedGridContainer.transform as RectTransform;

            var positionOnGrid = RectUtils.GetPositionOnGrid(
                cursorPosition: cursorPosition,
                gridTransform: gridTransform,
                itemVM: selectedItemVM);

            UpdateHighlight(positionOnGrid);
        }

        private void UpdateHighlighterParent()
        {
            var parent = selectedGridVM != null
                ? selectedGridContainer.HighlightArea
                : highlighterDefaultParent;

            highlighter.SetParent(parent);
        }

        private void UpdateHighlight()
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
            if (selectedGridVM == null || selectedItemVM == null)
            {
                highlighter.Show(false);
                return;
            }

            highlighter.SetState(GetHighlighterState(positionOnGrid));
            highlighter.SetBounds(new(positionOnGrid.Item, selectedItemVM.BoundsVM.Size));

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
                && !pickupResult.ThisPositionIsPositionOfSelectedItem(positionOnGrid, selectedGridVM)
                && selectedItemVM is IStackableItemVM stackableItemVM
                && stackableItemVM.CheckPossibilityOfSplit()
                && selectedGridVM.GetItem(positionOnGrid.Cursor) is null;
        }

        private bool SelectedItemCanBeTransferInHovered(in PositionOnGrid positionOnGrid)
        {
            var hoveredItemVM = selectedGridVM.GetItem(positionOnGrid.Cursor);

            return !pickupResult.ThisPositionIsPositionOfSelectedItem(positionOnGrid, selectedGridVM)
                && hoveredItemVM is IStackableItemVM stackableItemVM
                && stackableItemVM.CheckPossibilityOfTransfer(selectedItemVM);
        }

        private bool SelectedItemCanBePlaced(in PositionOnGrid positionOnGrid)
        {
            return selectedGridVM.CheckPossibilityOfPlacement(selectedItemVM, positionOnGrid.Item);
        }

        private bool SelectedItemCanBeInsidePlacement(in PositionOnGrid positionOnGrid)
        {
            var hoveredItemVM = selectedGridVM.GetItem(positionOnGrid.Cursor);

            return hoveredItemVM is IPlacementContainerVM placementVM
                && placementVM.CheckPossibilityOfPlacement(selectedItemVM);
        }

        private void UpdateHighlightedItem(in PositionOnGrid positionOnGrid)
        {
            var gridSelected = selectedGridVM != null;
            var equipSlotSelected = selectedSlotVM != null;

            if (gridSelected || equipSlotSelected)
            {
                if (gridSelected)
                {
                    var newHighlightedItemVM = selectedGridVM?.GetItem(positionOnGrid.Cursor);

                    if (newHighlightedItemVM != highlightedItemVM)
                    {
                        highlightedItemVM?.HighlightStateVM.ResetState();
                        highlightedItemVM = newHighlightedItemVM;
                        highlightedItemVM?.HighlightStateVM.SetState(highlightedState);
                    }
                }

                if (equipSlotSelected)
                {
                    if (highlightedItemVM == null)
                    {
                        highlightedItemVM?.HighlightStateVM.ResetState();
                        highlightedItemVM = selectedSlotVM?.ItemVM.Value;
                        highlightedItemVM?.HighlightStateVM.SetState(highlightedState);
                    }
                }
            }
            else if (highlightedItemVM != null)
            {
                highlightedItemVM.HighlightStateVM.ResetState();
                highlightedItemVM = null;
            }
        }
    }
}