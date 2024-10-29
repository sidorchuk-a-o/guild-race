using AD.Services.Router;
using Game.Input;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Game.Inventory
{
    public class SplittingItem : ReleaseHandler
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
                && inventoryInputs.SplittingModeOn.Value
                && hoveredItemVM is null
                && !result.Context.CurrentPositionIsPositionOfSelectedItem
                && selectedItemVM is IStackableItemVM stackableItemVM
                && stackableItemVM.CheckPossibilityOfSplit();
        }

        protected override async UniTask Process(ReleaseResult result)
        {
            var pickupResult = result.Context.PickupResult;

            var selectedItemVM = pickupResult.SelectedItemVM;
            var stackableItemVM = selectedItemVM as IStackableItemVM;
            var selectedStackVM = stackableItemVM.StackVM;

            var selectedGridVM = result.Context.SelectedGridVM;
            var positionOnGrid = result.Context.PositionOnSelectedGrid;

            if (stackableItemVM.StackVM.Value < 3)
            {
                var count = Mathf.CeilToInt(selectedStackVM.Value / 2f);

                inventoryVMF.TrySplitItem(new SplittingItemArgs
                {
                    SelectedItemId = selectedItemVM.Id,
                    GridId = selectedGridVM.Id,
                    PositionOnGrid = positionOnGrid.Item,
                    Count = count
                });
            }
            else
            {
                var completeTask = new UniTaskCompletionSource();

                var dialogKey = RouteKeys.Inventory.splittingItemDialog;
                var parameters = RouteParams.Default;

                parameters[StackableItemDialog.contextKey] = new StackableContext
                {
                    SelectedItemVM = selectedItemVM,
                    SelectedGridVM = selectedGridVM,
                    IsRotated = selectedItemVM.BoundsVM.IsRotated,
                    PositionOnGrid = positionOnGrid,
                    CompleteTask = completeTask
                };

                router.Push(dialogKey, parameters: parameters);

                await completeTask.Task;
            }
        }
    }
}