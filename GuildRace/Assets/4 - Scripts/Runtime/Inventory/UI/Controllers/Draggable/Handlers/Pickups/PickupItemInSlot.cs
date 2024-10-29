namespace Game.Inventory
{
    public class PickupItemInSlot : PickupHandler
    {
        protected override bool CheckContext(PickupResult result)
        {
            var itemHolderFromPickup = result.Context.SelectedSlotVM;

            return itemHolderFromPickup != null
                && itemHolderFromPickup.HasItem;
        }

        protected override void Process(PickupResult result)
        {
            var selectedSlotVM = result.Context.SelectedSlotVM;
            var pickupItemVM = result.Context.HoveredItemVM;

            if (selectedSlotVM.TryRemoveItem())
            {
                result.SelectedItemVM = pickupItemVM;
            }
        }
    }
}