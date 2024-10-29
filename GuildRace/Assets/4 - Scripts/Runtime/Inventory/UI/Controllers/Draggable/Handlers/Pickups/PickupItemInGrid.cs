namespace Game.Inventory
{
    public class PickupItemInGrid : PickupHandler
    {
        protected override bool CheckContext(PickupResult result)
        {
            return result.Context.SelectedGridVM != null;
        }

        protected override void Process(PickupResult result)
        {
            var selectedGridVM = result.Context.SelectedGridVM;
            var pickupItemVM = result.Context.HoveredItemVM;

            if (selectedGridVM.TryRemoveItem(pickupItemVM))
            {
                result.SelectedItemVM = pickupItemVM;
                result.Bounds = pickupItemVM.BoundsVM.Value;
                result.IsRotated = pickupItemVM.BoundsVM.IsRotated;
            }
        }
    }
}