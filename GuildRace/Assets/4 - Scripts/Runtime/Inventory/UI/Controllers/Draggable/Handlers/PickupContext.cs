namespace Game.Inventory
{
    public class PickupContext
    {
        public ItemVM HoveredItem { get; set; }

        public ItemsGridContainer SelectedGrid { get; set; }
        public ItemSlotContainer SelectedSlot { get; set; }
    }
}