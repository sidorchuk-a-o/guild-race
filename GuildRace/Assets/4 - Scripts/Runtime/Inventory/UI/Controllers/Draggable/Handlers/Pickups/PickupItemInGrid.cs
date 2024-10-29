namespace Game.Inventory
{
    public class PickupItemInGrid : PickupHandler
    {
        protected override bool CheckContext(PickupResult result)
        {
            return result.Context.SelectedGrid != null;
        }

        protected override void Process(PickupResult result)
        {
            var selectedGrid = result.Context.SelectedGrid;
            var pickupItemVM = result.Context.HoveredItem;

            if (selectedGrid.ViewModel.TryRemoveItem(pickupItemVM))
            {
                result.SelectedItem = pickupItemVM;
                result.Bounds = pickupItemVM.BoundsVM.Value;
                result.IsRotated = pickupItemVM.BoundsVM.IsRotated;
            }
        }
    }
}