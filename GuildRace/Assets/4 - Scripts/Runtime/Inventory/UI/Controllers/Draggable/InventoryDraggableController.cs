using AD.Services.Router;
using AD.ToolsCollection;
using Game.UI;
using Game.Input;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Inventory
{
    public class InventoryDraggableController : UIComponent<InventoryDraggableController>
    {
        private const string placeReadyState = "placeReady";
        private const string stackReadyState = "stackReady";

        [Header("Picked Item")]
        [SerializeField] private PickedItemPreview pickedItemPreview;
        [SerializeField] private PickedItemPreview pickedGridPreview;
        [SerializeField] private float dragMovementSpeed = 128f;

        private Transform previewDefaultParent;

        private readonly Subject<PickupResult> onPickupItem = new();
        private readonly Subject<ReleaseResult> onReleaseItem = new();

        private IInventoryInputModule inventoryInputs;

        private IReadOnlyList<PickupHandler> pickupHandlers;
        private IReadOnlyList<ReleaseHandler> placeHandlers;
        private IReadOnlyList<ReleaseHandler> splitHandlers;
        private IReadOnlyList<ReleaseHandler> rollbackHandlers;

        private ItemVM selectedItemVM;
        private ItemSlotVM selectedSlotVM;
        private ItemsGridVM selectedGridVM;

        private ItemsGridContainer selectedGridContainer;

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

        protected override void Awake()
        {
            base.Awake();

            previewDefaultParent = pickedGridPreview.transform.parent;
        }

        public void Init(CompositeDisp disp)
        {
            inventoryInputs.OnPickupItem
                .Subscribe(PickupItemCallback)
                .AddTo(disp);

            inventoryInputs.OnReleaseItem
                .Subscribe(ReleaseItemCallback)
                .AddTo(disp);

            inventoryInputs.OnRotateItem
                .Subscribe(RotateItemCallback)
                .AddTo(disp);

            inventoryInputs.SplittingModeOn
                .Subscribe(SplittingModeChangedCallback)
                .AddTo(disp);

            ItemSlotContainer.OnInited
                .Subscribe(ItemSlotInitedCallback)
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
            if (selectedItemVM == null)
            {
                return;
            }

            var cursorPosition = inventoryInputs.CursorPosition;
            var previewTransform = pickedItemPreview.transform as RectTransform;
            var positionT = dragMovementSpeed * Time.deltaTime;

            previewTransform.position = Vector3.Lerp(previewTransform.position, cursorPosition, positionT);
        }

        // == Pointer Callbacks ==

        private void ItemSlotInitedCallback(ItemSlotContainer itemSlot)
        {
            if (selectedItemVM == null)
            {
                return;
            }

            UpdateItemSlot(itemSlot);
        }

        private void ItemSlotInteractedCallback(ItemSlotContainer itemSlot)
        {
            if (itemSlot && itemSlot.IsReadOnly)
            {
                return;
            }

            selectedSlotVM = itemSlot?.ViewModel;
        }

        private void ItemsGridInteractedCallback(ItemsGridContainer itemsGrid)
        {
            selectedGridVM = itemsGrid?.ViewModel;
            selectedGridContainer = itemsGrid;
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
            selectedItemVM?.BoundsVM.Rotate();
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
                HoveredItemVM = hoveredItem,
                SelectedGridVM = selectedGridVM,
                SelectedSlotVM = selectedSlotVM
            };

            if (pickupCancelled)
            {
                return;
            }

            pickupResult = StartPickupProcess(pickupContext);
            selectedItemVM = pickupResult.SelectedItemVM;

            if (selectedItemVM == null)
            {
                pickupResult = null;

                return;
            }

            // TODO: play pickup sfx

            UpdateDragPreview();
            UpdateStackableItems();

            if (!inventoryInputs.SplittingModeOn.Value)
            {
                UpdateItemSlots();
                UpdatePlacementContainers();
            }

            onPickupItem.OnNext(pickupResult);
        }

        private void UpdateDragPreview()
        {
            const int defaultCellSize = 65;

            if (selectedGridContainer != null)
            {
                pickedGridPreview.SetParent(selectedGridContainer.PickedItemArea);
                pickedGridPreview.ShowItem(selectedItemVM, iconStaticOn: true, selectedGridContainer.CellSize);
            }

            pickedItemPreview.ShowItem(selectedItemVM, iconStaticOn: false, defaultCellSize);
        }

        private void UpdateItemSlots()
        {
            if (selectedItemVM == null)
            {
                return;
            }

            foreach (var itemSlot in ItemSlotContainer.EnabledComponents)
            {
                UpdateItemSlot(itemSlot);
            }
        }

        private void UpdateItemSlot(ItemSlotContainer itemSlot)
        {
            itemSlot.ShowPickupPreview(selectedItemVM, pickupResult);
        }

        private void UpdatePlacementContainers()
        {
            if (selectedItemVM == null)
            {
                return;
            }

            foreach (var itemSlot in ItemSlotContainer.EnabledComponents)
            {
                if (itemSlot.ViewModel.ItemVM.Value is IPlacementContainerVM placementVM)
                {
                    updatePlacementState(placementVM);

                    foreach (var child in placementVM.GetPlacementsInChildren())
                    {
                        updatePlacementState(child);
                    }
                }
            }

            void updatePlacementState(IPlacementContainerVM placementVM)
            {
                var check = placementVM.CheckPossibilityOfPlacement(selectedItemVM);
                var state = check ? placeReadyState : string.Empty;

                placementVM.PlacementStateVM.SetState(state);
            }
        }

        private void UpdateStackableItems()
        {
            if (selectedItemVM == null)
            {
                return;
            }

            foreach (var item in ItemInGridComponent.EnabledComponents)
            {
                if (item.ViewModel is IStackableItemVM stackableVM)
                {
                    var check = stackableVM.CheckPossibilityOfTransfer(selectedItemVM);
                    var state = check ? stackReadyState : string.Empty;

                    stackableVM.StackableStateVM.SetState(state);
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

                if (result.SelectedItemVM != null)
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
                SelectedGridVM = selectedGridVM,
                SelectedSlotVM = selectedSlotVM,
                HoveredItemVM = GetHoveredItem(),
                PositionOnSelectedGrid = GetPositionOnSelectedGrid(),
                CurrentPositionIsPositionOfSelectedItem = CurrentPositionIsPositionOfSelectedItem()
            };

            ResetDragPreview();
            ResetItemSlots();
            ResetPlacementContainers();
            ResetStackableItems();

            selectedItemVM = null;
            pickupResult = null;

            // release
            var releaseResult = await StartPlaceProcess(releaseContext);

            // TODO: play place sfx

            onReleaseItem.OnNext(releaseResult);
        }

        public ItemVM GetHoveredItem()
        {
            // slot
            if (selectedSlotVM != null && selectedSlotVM.HasItem)
            {
                return selectedSlotVM.ItemVM.Value;
            }

            // grid
            if (selectedGridVM != null)
            {
                var positionOnGrid = GetPositionOnSelectedGrid();

                return selectedGridVM.GetItem(positionOnGrid.Cursor);
            }

            return null;
        }

        public PositionOnGrid GetPositionOnSelectedGrid()
        {
            if (selectedGridVM == null)
            {
                return default;
            }

            var selectedItem = pickupResult?.SelectedItemVM;
            var cursorPosition = inventoryInputs.CursorPosition;

            var positionOnGrid = RectUtils.GetPositionOnGrid(
                cursorPosition: cursorPosition,
                gridContainer: selectedGridContainer,
                itemVM: selectedItem);

            return positionOnGrid;
        }

        public bool CurrentPositionIsPositionOfSelectedItem()
        {
            if (selectedGridVM == null || pickupResult == null)
            {
                return false;
            }

            var positionOnGrid = GetPositionOnSelectedGrid();

            return pickupResult.ThisPositionIsPositionOfSelectedItem(positionOnGrid, selectedGridVM);
        }

        private void ResetDragPreview()
        {
            pickedGridPreview.SetParent(previewDefaultParent);
            pickedItemPreview.SetParent(previewDefaultParent);

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
                if (itemSlot.ViewModel.ItemVM.Value is IPlacementContainerVM placementVM)
                {
                    placementVM.PlacementStateVM.ResetState();

                    foreach (var child in placementVM.GetPlacementsInChildren())
                    {
                        child.PlacementStateVM.ResetState();
                    }
                }
            }
        }

        private void ResetStackableItems()
        {
            foreach (var item in ItemInGridComponent.EnabledComponents)
            {
                if (item.ViewModel is IStackableItemVM stackableVM)
                {
                    stackableVM.StackableStateVM.ResetState();
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