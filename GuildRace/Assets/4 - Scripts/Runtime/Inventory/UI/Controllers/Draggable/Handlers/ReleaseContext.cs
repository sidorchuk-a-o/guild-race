namespace Game.Inventory
{
    public class ReleaseContext
    {
        public PickupResult PickupResult { get; set; }

        public ItemsGridVM SelectedGridVM { get; set; }
        public ItemSlotVM SelectedSlotVM { get; set; }

        public ItemVM HoveredItemVM { get; set; }
        public PositionOnGrid PositionOnSelectedGrid { get; set; }
        public bool CurrentPositionIsPositionOfSelectedItem { get; set; }
    }
}