using Cysharp.Threading.Tasks;

namespace AD.Game.Inventory
{
    public class PlaceItemInSlot : ReleaseHandler
    {
        protected override bool CheckContext(ReleaseResult result)
        {
            return result.Context.SelectedSlotVM != null;
        }

        protected override UniTask Process(ReleaseResult result)
        {
            var pickupResult = result.Context.PickupResult;

            var selectedItemVM = pickupResult.SelectedItemVM;
            var selectedSlotVM = result.Context.SelectedSlotVM;

            var prevItem = selectedSlotVM.ItemVM.CurrentValue;
            var prevItemExist = selectedSlotVM.HasItem;

            if (prevItemExist)
            {
                selectedSlotVM.TryRemoveItem();
            }

            result.Placed = selectedSlotVM.TryAddItem(selectedItemVM);

            if (result.Placed && prevItemExist)
            {
                var prevPlaced = false;
                var prevPosition = pickupResult.Bounds.position;
                var prevContainer = pickupResult.Context.SelectedGridVM;
                var prevSlot = pickupResult.Context.SelectedSlotVM;

                if (prevContainer != null)
                {
                    prevPlaced = prevContainer.TryPlaceItem(prevItem, prevPosition);

                    if (!prevPlaced)
                    {
                        prevPlaced = prevContainer.TryPlaceItem(prevItem);
                    }
                }

                if (prevSlot != null)
                {
                    prevPlaced = prevSlot.TryAddItem(prevItem);
                }

                result.Placed = prevPlaced;
            }
            else if (!result.Placed && prevItemExist)
            {
                selectedSlotVM.TryAddItem(prevItem);
            }

            return UniTask.CompletedTask;
        }
    }
}