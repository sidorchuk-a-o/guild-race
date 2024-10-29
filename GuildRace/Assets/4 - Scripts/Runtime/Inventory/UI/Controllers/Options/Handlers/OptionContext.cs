namespace Game.Inventory
{
    public class OptionContext
    {
        public string SelectedItemId { get; set; }

        public ItemsGridVM SelectedGridVM { get; set; }
        public ItemSlotVM SelectedSlotVM { get; set; }
    }
}