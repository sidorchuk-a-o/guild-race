using AD.Services.Router;
using Game.Input;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Game.Inventory
{
    public class TransferItem : ReleaseHandler
    {
        private IRouterService router;
        private IInventoryInputModule inventoryInputs;

        private InventoryVMFactory inventoryVMF;

        [Inject]
        public void Inject(
            IRouterService router,
            IInputService inputService,
            InventoryVMFactory inventoryVMF)
        {
            this.router = router;
            this.inventoryVMF = inventoryVMF;

            inventoryInputs = inputService.InventoryModule;
        }

        protected override bool CheckContext(ReleaseResult result)
        {
            var selectedGridVM = result.Context.SelectedGridVM;
            var selectedItemVM = result.Context.PickupResult.SelectedItemVM;
            var hoveredItemVM = result.Context.HoveredItemVM;

            return selectedGridVM != null
                && !result.Context.CurrentPositionIsPositionOfSelectedItem
                && hoveredItemVM is IStackableItemVM stackableItemVM
                && stackableItemVM.CheckPossibilityOfTransfer(selectedItemVM);
        }

        protected override async UniTask Process(ReleaseResult result)
        {
            var selectedItemVM = result.Context.PickupResult.SelectedItemVM;
            var selectedStackableVM = selectedItemVM as IStackableItemVM;
            var selectedStackVM = selectedStackableVM.StackVM;

            var hoveredItemVM = result.Context.HoveredItemVM;
            var hoveredStackableVM = hoveredItemVM as IStackableItemVM;
            var hoveredStackVM = hoveredStackableVM.StackVM;

            var transferCount = Mathf.Min(
                a: selectedStackVM.Value,
                b: hoveredStackVM.AvailableSpace);

            var selectedGridVM = result.Context.SelectedGridVM;
            var positionOnGrid = result.Context.PositionOnSelectedGrid;

            if (inventoryInputs.SplittingModeOn.Value && transferCount > 2)
            {
                var completeTask = new UniTaskCompletionSource();

                var dialogKey = RouteKeys.Inventory.TransferItemDialog;
                var parameters = RouteParams.Default;

                parameters[StackableItemDialog.contextKey] = new StackableContext
                {
                    SelectedItemVM = selectedItemVM,
                    HoveredItemVM = hoveredItemVM,
                    SelectedGridVM = selectedGridVM,
                    IsRotated = selectedItemVM.BoundsVM.IsRotated,
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
                    SourceItemId = selectedItemVM.Id,
                    TargetItemId = hoveredItemVM.Id,
                    Count = transferCount
                });
            }
        }
    }
}