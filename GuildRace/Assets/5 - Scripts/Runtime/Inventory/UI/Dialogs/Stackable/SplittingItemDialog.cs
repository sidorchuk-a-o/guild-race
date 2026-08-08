namespace AD.Game.Inventory
{
    public class SplittingItemDialog : StackableItemDialog
    {
        protected override void OkCallback()
        {
            var selectedItemVM = Context.SelectedItemVM;
            var selectedGridVM = Context.SelectedGridVM;

            InventoryVMF.TrySplitItem(new()
            {
                SelectedItemId = selectedItemVM.Id,
                GridId = selectedGridVM.Id,
                IsRotated = Context.IsRotated,
                PositionOnGrid = Context.PositionOnGrid.Item,
                Count = Size
            });

            base.OkCallback();
        }
    }
}