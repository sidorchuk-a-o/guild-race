#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using Cysharp.Threading.Tasks;

namespace Game.Inventory
{
    public class PlaceItemInSlot : ReleaseHandler
    {
        protected override bool CheckContext(ReleaseResult result)
        {
            return result.Context.SelectedSlotVM != null;
        }

        protected override async UniTask Process(ReleaseResult result)
        {
            var pickupResult = result.Context.PickupResult;

            var selectedItemVM = pickupResult.SelectedItemVM;
            var selectedSlotVM = result.Context.SelectedSlotVM;

            var prevItem = selectedSlotVM.ItemVM.Value;
            var prevContainer = pickupResult.Context.SelectedGridVM;
            var prevPosition = pickupResult.Bounds.position;

            result.Placed = selectedSlotVM.TryAddItem(selectedItemVM);

            if (result.Placed && prevItem != null)
            {
                var prevPlaced = prevContainer.TryPlaceItem(prevItem, prevPosition);

                if (prevPlaced == false)
                {
                    prevPlaced = prevContainer.TryPlaceItem(prevItem);
                }

                result.Placed = prevPlaced;
            }
        }
    }
}