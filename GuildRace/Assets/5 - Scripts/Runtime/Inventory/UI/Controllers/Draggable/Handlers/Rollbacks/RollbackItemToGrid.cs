using Cysharp.Threading.Tasks;

namespace AD.Game.Inventory
{
    public class RollbackItemToGrid : ReleaseHandler
    {
        protected override bool CheckContext(ReleaseResult result)
        {
            return result.Context.PickupResult.Context.SelectedGridVM != null;
        }

        protected override UniTask Process(ReleaseResult result)
        {
            var pickupResult = result.Context.PickupResult;
            var pickupContext = pickupResult.Context;

            var selectedItemVM = pickupResult.SelectedItemVM;
            var selectedGridVM = pickupContext.SelectedGridVM;

            if (selectedItemVM.BoundsVM.IsRotated != pickupResult.IsRotated)
            {
                selectedItemVM.BoundsVM.Rotate();
            }

            result.Placed = selectedGridVM.TryPlaceItem(selectedItemVM, pickupResult.Bounds.position);

            return UniTask.CompletedTask;
        }
    }
}