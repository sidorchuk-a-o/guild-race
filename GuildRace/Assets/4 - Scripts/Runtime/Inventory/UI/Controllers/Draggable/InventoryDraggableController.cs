using AD.Services.Router;
using AD.ToolsCollection;
using Game.Input;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Inventory
{
    public class InventoryDraggableController : MonoBehaviour
    {
        private const string slotReadyState = "slotReady";
        private const string slotPreviewState = "slotPreview";
        private const string placeReadyState = "placeReady";
        private const string stackReadyState = "stackReady";

        [Header("Picked Item")]
        [SerializeField] private PickedItemPreview pickedItemPreview;
        [SerializeField] private PickedItemPreview pickedGridPreview;
        [SerializeField] private float dragMovementSpeed = 128f;

        private readonly Subject<PickupResult> onPickupItem = new();
        private readonly Subject<ReleaseResult> onReleaseItem = new();

        private IInventoryInputModule inventoryInputs;

        private IReadOnlyList<PickupHandler> pickupHandlers;
        private IReadOnlyList<ReleaseHandler> placeHandlers;
        private IReadOnlyList<ReleaseHandler> splitHandlers;
        private IReadOnlyList<ReleaseHandler> rollbackHandlers;

        private ItemVM selectedItem;
        private ItemSlotContainer selectedSlot;
        private ItemsGridContainer selectedGrid;

        private bool pickupCancelled;
        private PickupResult pickupResult;

        public IObservable<PickupResult> OnPickupItem => onPickupItem;
        public IObservable<ReleaseResult> OnReleaseItem => onReleaseItem;

        [Inject]
        public void Inject(InventoryConfig config, IInputService inputService, IObjectResolver resolver)
        {
            inventoryInputs = inputService.InventoryModule;

            pickupHandlers = config.UIParams.PickupHandlers;
            placeHandlers = config.UIParams.PlaceHandlers;
            splitHandlers = config.UIParams.SplitHandlers;
            rollbackHandlers = config.UIParams.RollbackHandlers;

            pickupHandlers.ForEach(resolver.Inject);
            placeHandlers.ForEach(resolver.Inject);
            splitHandlers.ForEach(resolver.Inject);
            rollbackHandlers.ForEach(resolver.Inject);
        }

        public void Init(CompositeDisp disp)
        {
            inventoryInputs.SplittingModeOn
                .Subscribe(SplittingModeChangedCallback)
                .AddTo(disp);

            inventoryInputs.OnRotateItem
                .Subscribe(RotateItemCallback)
                .AddTo(disp);

            inventoryInputs.OnPickupItem
                .Subscribe(PickupItemCallback)
                .AddTo(disp);

            inventoryInputs.OnReleaseItem
                .Subscribe(ReleaseItemCallback)
                .AddTo(disp);

            ItemSlotContainer.OnInteracted
                .Subscribe(ItemSlotInteractedCallback)
                .AddTo(disp);

            ItemsGridContainer.OnInteracted
                .Subscribe(ItemsGridInteractedCallback)
                .AddTo(disp);
        }

        private void Update()
        {
            UpdateItemPreviewPosition();
        }

        private void UpdateItemPreviewPosition()
        {
            if (selectedItem == null)
            {
                return;
            }

            var cursorPosition = inventoryInputs.CursorPosition;
            var previewTransform = pickedItemPreview.transform as RectTransform;
            var positionT = dragMovementSpeed * Time.deltaTime;

            previewTransform.position = Vector3.Lerp(previewTransform.position, cursorPosition, positionT);
        }

        // == Pointer Callbacks ==

        private void ItemSlotInteractedCallback(ItemSlotContainer itemSlot)
        {
            selectedSlot = itemSlot;
        }

        private void ItemsGridInteractedCallback(ItemsGridContainer itemsGrid)
        {
            selectedGrid = itemsGrid;
        }

        // == Split Or Transfer Item ==

        private void SplittingModeChangedCallback(bool modeOn)
        {
            if (pickupResult == null)
            {
                return;
            }

            if (modeOn)
            {
                ResetItemSlots();
                ResetPlacementContainers();
            }
            else
            {
                UpdateItemSlots();
                UpdatePlacementContainers();
            }
        }

        // == Pickup Item ==

        private void RotateItemCallback()
        {
            selectedItem?.BoundsVM.Rotate();
        }

        private void PickupItemCallback()
        {
            pickupCancelled = false;

            if (pickupResult != null)
            {
                return;
            }

            var hoveredItem = GetHoveredItem();

            if (hoveredItem == null)
            {
                return;
            }

            var pickupContext = new PickupContext
            {
                HoveredItem = hoveredItem,
                SelectedGrid = selectedGrid,
                SelectedSlot = selectedSlot
            };

            if (pickupCancelled)
            {
                return;
            }

            pickupResult = StartPickupProcess(pickupContext);
            selectedItem = pickupResult.SelectedItem;

            if (selectedItem == null)
            {
                pickupResult = null;

                return;
            }

            // TODO: play pickup sfx

            UpdateDragPreview(pickupResult);
            UpdateStackableItems();

            if (!inventoryInputs.SplittingModeOn.Value)
            {
                UpdateItemSlots();
                UpdatePlacementContainers();
            }

            onPickupItem.OnNext(pickupResult);
        }

        private void UpdateDragPreview(PickupResult pickupResult)
        {
            var selectedGrid = pickupResult.Context.SelectedGrid;

            if (selectedGrid != null)
            {
                pickedGridPreview.SetParent(selectedGrid.PickedItemArea);
                pickedGridPreview.ShowItem(selectedItem, iconStaticOn: true);
            }

            pickedItemPreview.ShowItem(selectedItem, iconStaticOn: false);
        }

        private void UpdateItemSlots()
        {
            if (selectedItem == null)
            {
                return;
            }

            foreach (var itemSlot in ItemSlotContainer.EnabledComponents)
            {
                if (itemSlot.ViewModel.CheckPossibilityOfPlacement(selectedItem))
                {
                    var state = pickupResult.Context.SelectedSlot == itemSlot
                        ? slotPreviewState
                        : slotReadyState;

                    itemSlot.ShowPickupPreview(selectedItem, state);
                }
            }
        }

        private void UpdatePlacementContainers()
        {
            if (selectedItem == null)
            {
                return;
            }

            foreach (var itemSlot in ItemSlotContainer.EnabledComponents)
            {
                if (itemSlot.ViewModel.ItemVM.Value is IPlacementContainerVM placement)
                {
                    updatePlacementState(placement);

                    foreach (var child in placement.GetPlacementsInChildren())
                    {
                        updatePlacementState(child);
                    }
                }
            }

            void updatePlacementState(IPlacementContainerVM placement)
            {
                var check = placement.CheckPossibilityOfPlacement(selectedItem);
                var state = check ? placeReadyState : string.Empty;

                placement.PlacementState.SetState(state);
            }
        }

        private void UpdateStackableItems()
        {
            if (selectedItem == null)
            {
                return;
            }

            foreach (var item in ItemInGridComponent.EnabledComponents)
            {
                if (item.ViewModel is IStackableItemVM stackable)
                {
                    var check = stackable.CheckPossibilityOfTransfer(selectedItem);
                    var state = check ? stackReadyState : string.Empty;

                    stackable.StackableState.SetState(state);
                }
            }
        }

        private PickupResult StartPickupProcess(PickupContext pickupContext)
        {
            var result = new PickupResult
            {
                Context = pickupContext
            };

            foreach (var pickupHandler in pickupHandlers)
            {
                pickupHandler.StartProcess(result);

                if (result.SelectedItem != null)
                {
                    break;
                }
            }

            return result;
        }

        private async void ReleaseItemCallback()
        {
            pickupCancelled = true;

            if (pickupResult == null)
            {
                return;
            }

            var releaseContext = new ReleaseContext
            {
                PickupResult = pickupResult,
                SelectedGrid = selectedGrid,
                SelectedSlot = selectedSlot,
                HoveredItem = GetHoveredItem(),
                PositionOnSelectedGrid = GetPositionOnSelectedGrid(),
                CurrentPositionIsPositionOfSelectedItem = CurrentPositionIsPositionOfSelectedItem()
            };

            ResetDragPreview();
            ResetItemSlots();
            ResetPlacementContainers();
            ResetStackableItems();

            selectedItem = null;
            pickupResult = null;

            // release
            var releaseResult = await StartPlaceProcess(releaseContext);

            // TODO: play place sfx

            onReleaseItem.OnNext(releaseResult);
        }

        public ItemVM GetHoveredItem()
        {
            // slot
            if (selectedSlot != null && selectedSlot.HasItem)
            {
                return selectedSlot.ViewModel.ItemVM.Value;
            }

            // grid
            if (selectedGrid != null)
            {
                var positionOnGrid = GetPositionOnSelectedGrid();

                return selectedGrid.ViewModel.GetItem(positionOnGrid.Cursor);
            }

            return null;
        }

        public PositionOnGrid GetPositionOnSelectedGrid()
        {
            if (selectedGrid == null)
            {
                return default;
            }

            var selectedItem = pickupResult?.SelectedItem;
            var cursorPosition = inventoryInputs.CursorPosition;
            var gridTransform = selectedGrid.transform as RectTransform;

            var positionOnGrid = RectUtils.GetPositionOnGrid(
                cursorPosition: cursorPosition,
                gridTransform: gridTransform,
                selectedItem: selectedItem);

            return positionOnGrid;
        }

        public bool CurrentPositionIsPositionOfSelectedItem()
        {
            if (selectedGrid == null || pickupResult == null)
            {
                return false;
            }

            var positionOnGrid = GetPositionOnSelectedGrid();

            return pickupResult.ThisPositionIsPositionOfSelectedItem(positionOnGrid, selectedGrid);
        }

        private void ResetDragPreview()
        {
            pickedGridPreview.SetActive(false);
            pickedItemPreview.SetActive(false);
        }

        private void ResetItemSlots()
        {
            foreach (var itemSlot in ItemSlotContainer.EnabledComponents)
            {
                itemSlot.ResetPickupPreview();
            }
        }

        private void ResetPlacementContainers()
        {
            foreach (var itemSlot in ItemSlotContainer.EnabledComponents)
            {
                if (itemSlot.ViewModel.ItemVM.Value is IPlacementContainerVM placement)
                {
                    placement.PlacementState.ResetState();

                    foreach (var child in placement.GetPlacementsInChildren())
                    {
                        child.PlacementState.ResetState();
                    }
                }
            }
        }

        private void ResetStackableItems()
        {
            foreach (var item in ItemInGridComponent.EnabledComponents)
            {
                if (item.ViewModel is IStackableItemVM stackable)
                {
                    stackable.StackableState.ResetState();
                }
            }
        }

        private async UniTask<ReleaseResult> StartPlaceProcess(ReleaseContext releaseContext)
        {
            var result = new ReleaseResult
            {
                Context = releaseContext
            };

            // place
            if (inventoryInputs.SplittingModeOn.Value)
            {
                if (await StartProcess(splitHandlers, result))
                {
                    return result;
                }
            }
            else
            {
                if (await StartProcess(placeHandlers, result))
                {
                    return result;
                }
            }

            // rollback
            if (await StartProcess(rollbackHandlers, result))
            {
                return result;
            }

            return result;
        }

        private async UniTask<bool> StartProcess(IEnumerable<ReleaseHandler> handlers, ReleaseResult result)
        {
            foreach (var handler in handlers)
            {
                await handler.StartProcess(result);

                if (result.Placed)
                {
                    return true;
                }
            }

            return false;
        }
    }
}