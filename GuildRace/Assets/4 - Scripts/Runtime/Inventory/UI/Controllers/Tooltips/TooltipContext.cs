namespace Game.Inventory
{
    public class TooltipContext
    {
        public ItemVM ItemVM { get; }
        public ItemDataVM DataVM { get; }

        public TooltipContext(ItemDataVM dataVM)
        {
            DataVM = dataVM;
        }

        public TooltipContext(ItemVM itemVM)
        {
            ItemVM = itemVM;
            DataVM = itemVM.DataVM;
        }
    }
}