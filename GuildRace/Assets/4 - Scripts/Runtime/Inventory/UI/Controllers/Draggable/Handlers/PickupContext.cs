namespace Game.Inventory
{
    public class PickupContext
    {
        public ItemVM HoveredItemVM { get; set; }

        public ItemsGridVM SelectedGridVM { get; set; }
        public ItemSlotVM SelectedSlotVM { get; set; }
    }
}