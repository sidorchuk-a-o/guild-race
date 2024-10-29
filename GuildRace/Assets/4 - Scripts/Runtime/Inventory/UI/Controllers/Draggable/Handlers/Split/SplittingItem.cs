using AD.Services.Router;
using Game.Input;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Game.Inventory
{
    public class SplittingItem : ReleaseHandler
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
                && inventoryInputs.SplittingModeOn.Value
                && hoveredItem is null
                && !result.Context.CurrentPositionIsPositionOfSelectedItem
                && selectedItem is IStackableItemVM stackableItem
                && stackableItem.CheckPossibilityOfSplit();
        }

        protected override async UniTask Process(ReleaseResult result)
        {
            var pickupResult = result.Context.PickupResult;

            var selectedItem = pickupResult.SelectedItem;
            var stackableItem = selectedItem as IStackableItemVM;
            var selectedStack = stackableItem.Stack;

            var selectedGridVM = result.Context.SelectedGrid.ViewModel;
            var positionOnGrid = result.Context.PositionOnSelectedGrid;

            if (stackableItem.Stack.Value < 3)
            {
                var count = Mathf.CeilToInt(selectedStack.Value / 2f);

                inventoryVMF.TrySplitItem(new SplittingItemArgs
                {
                    SelectedItemId = selectedItem.Id,
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
                    SelectedItem = selectedItem,
                    SelectedGrid = selectedGridVM,
                    IsRotated = selectedItem.BoundsVM.IsRotated,
                    PositionOnGrid = positionOnGrid,
                    CompleteTask = completeTask
                };

                router.Push(dialogKey, parameters: parameters);

                await completeTask.Task;
            }
        }
    }
}