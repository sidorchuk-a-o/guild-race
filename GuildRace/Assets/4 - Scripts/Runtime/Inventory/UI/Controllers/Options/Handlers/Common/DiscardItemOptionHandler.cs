using AD.Services.Router;
using Cysharp.Threading.Tasks;
using System;
using VContainer;

namespace Game.Inventory
{
    public class DiscardItemOptionHandler : OptionHandler
    {
        private IRouterService router;
        private InventoryVMFactory inventoryVMF;

        [Inject]
        public void Inject(IRouterService router, InventoryVMFactory inventoryVMF)
        {
            this.router = router;
            this.inventoryVMF = inventoryVMF;
        }

        public override async UniTask StartProcess(OptionContext context)
        {
            var discardArgs = new DiscardItemArgs
            {
                ItemId = context.SelectedItemId,
                SlotId = context.SelectedSlotVM?.Id,
                PlacementId = context.SelectedGridVM?.Id
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