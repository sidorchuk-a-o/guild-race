using AD.Services.Router;
using Cysharp.Threading.Tasks;
using System;
using VContainer;

namespace Game.Inventory
{
    public class DiscardItemOptionHandler : OptionHandler
    {
        private InventoryVMFactory inventoryVMF;
        private IRouterService router;

        [Inject]
        public void Inject(InventoryVMFactory inventoryVMF, IRouterService router)
        {
            this.inventoryVMF = inventoryVMF;
            this.router = router;
        }

        public override async UniTask StartProcess(OptionContext context)
        {
            var discardArgs = new DiscardItemArgs
            {
                ItemId = context.SelectedItemId,
                SlotId = context.SelectedSlot?.ViewModel.Id,
                PlacementId = context.SelectedGrid?.ViewModel.Id
            };

            var parameters = RouteParams.Default;
            var completeTask = new UniTaskCompletionSource();

            parameters[DiscardItemDialog.itemIdKey] = discardArgs.ItemId;
            parameters[DiscardItemDialog.okKey] = (Action)(() =>
            {
                inventoryVMF.TryDiscardItem(discardArgs);

                completeTask.TrySetResult();
            });
            parameters[DiscardItemDialog.cancelKey] = (Action)(() =>
            {
                completeTask.TrySetResult();
            });

            var dialogKey = RouteKeys.Inventory.discardItemDialog;

            router.Push(dialogKey, parameters: parameters);

            await completeTask.Task;
        }
    }
}