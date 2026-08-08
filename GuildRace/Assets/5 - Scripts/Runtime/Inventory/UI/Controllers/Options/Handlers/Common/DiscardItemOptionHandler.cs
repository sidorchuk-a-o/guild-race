using System;
using AD.Router;
using AD.UI;
using Cysharp.Threading.Tasks;
using VContainer;

namespace AD.Game.Inventory
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

            parameters[DiscardItemDialog.ItemIdKey] = discardArgs.ItemId;
            parameters[BaseDialog.OkKey] = (Action)(() =>
            {
                inventoryVMF.TryDiscardItem(discardArgs);

                completeTask.TrySetResult();
            });
            parameters[BaseDialog.CancelKey] = (Action)(() =>
            {
                completeTask.TrySetResult();
            });

            var dialogKey = RouteKeys.Inventory.DiscardItemDialog;

            router.Push(dialogKey, parameters: parameters);

            await completeTask.Task;
        }
    }
}