namespace Game.Inventory
{
    public class ReleaseContext
    {
        public PickupResult PickupResult { get; set; }

        public ItemsGridContainer SelectedGrid { get; set; }
        public ItemSlotContainer SelectedSlot { get; set; }

        public ItemVM HoveredItem { get; set; }
        public PositionOnGrid PositionOnSelectedGrid { get; set; }
        public bool CurrentPositionIsPositionOfSelectedItem { get; set; }
    }
}