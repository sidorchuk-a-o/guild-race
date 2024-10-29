namespace Game.Inventory
{
    public class SplittingItemDialog : StackableItemDialog
    {
        protected override void OkCallback()
        {
            var selectedItem = Context.SelectedItem;
            var selectedGrid = Context.SelectedGrid;

            InventoryVMF.TrySplitItem(new SplittingItemArgs
            {
                SelectedItemId = selectedItem.Id,
                GridId = selectedGrid.Id,
                IsRotated = Context.IsRotated,
                PositionOnGrid = Context.PositionOnGrid.Item,
                Count = Size
            });

            base.OkCallback();
        }
    }
}