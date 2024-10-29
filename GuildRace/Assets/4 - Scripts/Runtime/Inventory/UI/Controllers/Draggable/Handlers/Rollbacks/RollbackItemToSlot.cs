#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using Cysharp.Threading.Tasks;

namespace Game.Inventory
{
    public class RollbackItemToSlot : ReleaseHandler
    {
        protected override bool CheckContext(ReleaseResult result)
        {
            return result.Context.PickupResult.Context.SelectedSlotVM != null;
        }

        protected override async UniTask Process(ReleaseResult result)
        {
            var pickupResult = result.Context.PickupResult;
            var pickupContext = pickupResult.Context;

            var selectedItemVM = pickupResult.SelectedItemVM;
            var selectedSlotVM = pickupContext.SelectedSlotVM;

            result.Placed = selectedSlotVM.TryAddItem(selectedItemVM);
        }
    }
}