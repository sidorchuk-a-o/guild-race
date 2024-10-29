namespace Game.Inventory
{
    public class PickupItemInSlot : PickupHandler
    {
        protected override bool CheckContext(PickupResult result)
        {
            var itemHolderFromPickup = result.Context.SelectedSlot;

            return itemHolderFromPickup != null
                && itemHolderFromPickup.HasItem;
        }

        protected override void Process(PickupResult result)
        {
            var selectedSlot = result.Context.SelectedSlot;
            var pickupItemVM = result.Context.HoveredItem;

            if (selectedSlot.ViewModel.TryRemoveItem())
            {
                result.SelectedItem = pickupItemVM;
            }
        }
    }
}