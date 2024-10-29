namespace Game.Inventory
{
    public class OptionContext
    {
        public string SelectedItemId { get; set; }

        public ItemsGridContainer SelectedGrid { get; set; }
        public ItemSlotContainer SelectedSlot { get; set; }
    }
}