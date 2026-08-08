using Cysharp.Threading.Tasks;

namespace AD.Game.Inventory
{
    public class RollbackItemToSlot : ReleaseHandler
    {
        protected override bool CheckContext(ReleaseResult result)
        {
            return result.Context.PickupResult.Context.SelectedSlotVM != null;
        }

        protected override UniTask Process(ReleaseResult result)
        {
            var pickupResult = result.Context.PickupResult;
            var pickupContext = pickupResult.Context;

            var selectedItemVM = pickupResult.SelectedItemVM;
            var selectedSlotVM = pickupContext.SelectedSlotVM;

            result.Placed = selectedSlotVM.TryAddItem(selectedItemVM);

            return UniTask.CompletedTask;
        }
    }
}