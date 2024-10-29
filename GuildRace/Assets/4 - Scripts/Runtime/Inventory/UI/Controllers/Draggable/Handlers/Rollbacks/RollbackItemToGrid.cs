#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using Cysharp.Threading.Tasks;

namespace Game.Inventory
{
    public class RollbackItemToGrid : ReleaseHandler
    {
        protected override bool CheckContext(ReleaseResult result)
        {
            return result.Context.PickupResult.Context.SelectedGrid != null;
        }

        protected override async UniTask Process(ReleaseResult result)
        {
            var pickupResult = result.Context.PickupResult;
            var pickupContext = pickupResult.Context;

            var selectedItem = pickupResult.SelectedItem;
            var selectedGrid = pickupContext.SelectedGrid;

            if (selectedItem.BoundsVM.IsRotated != pickupResult.IsRotated)
            {
                selectedItem.BoundsVM.Rotate();
            }

            result.Placed = selectedGrid.ViewModel.TryPlaceItem(selectedItem, pickupResult.Bounds.position);
        }
    }
}