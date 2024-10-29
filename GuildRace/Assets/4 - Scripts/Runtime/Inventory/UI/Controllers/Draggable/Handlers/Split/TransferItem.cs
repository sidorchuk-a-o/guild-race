using AD.Services.Router;
using Game.Input;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Game.Inventory
{
    public class TransferItem : ReleaseHandler
    {
        private InventoryVMFactory inventoryVMF;

        private IRouterService router;
        private IInventoryInputModule inventoryInputs;

        [Inject]
        public void Inject(
            InventoryVMFactory inventoryVMF,
            IRouterService router,
            IInputService inputService)
        {
            this.inventoryVMF = inventoryVMF;
            this.router = router;

            inventoryInputs = inputService.InventoryModule;
        }

        protected override bool CheckContext(ReleaseResult result)
        {
            var selectedGrid = result.Context.SelectedGrid;
            var selectedItem = result.Context.PickupResult.SelectedItem;
            var hoveredItem = result.Context.HoveredItem;

            return selectedGrid != null
                && !result.Context.CurrentPositionIsPositionOfSelectedItem
                && hoveredItem is IStackableItemVM stackableItem
                && stackableItem.CheckPossibilityOfTransfer(selectedItem);
        }

        protected override async UniTask Process(ReleaseResult result)
        {
            var selectedItem = result.Context.PickupResult.SelectedItem;
            var selectedStackable = selectedItem as IStackableItemVM;
            var selectedStack = selectedStackable.Stack;

            var hoveredItem = result.Context.HoveredItem;
            var hoveredStackable = hoveredItem as IStackableItemVM;
            var hoveredStack = hoveredStackable.Stack;

            var transferCount = Mathf.Min(
                a: selectedStack.Value,
                b: hoveredStack.AvailableSpace);

            var selectedGridVM = result.Context.SelectedGrid.ViewModel;
            var positionOnGrid = result.Context.PositionOnSelectedGrid;

            if (inventoryInputs.SplittingModeOn.Value && transferCount > 2)
            {
                var completeTask = new UniTaskCompletionSource();

                var dialogKey = RouteKeys.Inventory.transferItemDialog;
                var parameters = RouteParams.Default;

                parameters[StackableItemDialog.contextKey] = new StackableContext
                {
                    SelectedItem = selectedItem,
                    HoveredItem = hoveredItem,
                    SelectedGrid = selectedGridVM,
                    IsRotated = selectedItem.BoundsVM.IsRotated,
                    PositionOnGrid = positionOnGrid,
                    CompleteTask = completeTask
                };

                router.Push(dialogKey, parameters: parameters);

                await completeTask.Task;
            }
            else
            {
                inventoryVMF.TryTransferItem(new TransferItemArgs
                {
                    SourceItemId = selectedItem.Id,
                    TargetItemId = hoveredItem.Id,
                    Count = transferCount
                });
            }
        }
    }
}