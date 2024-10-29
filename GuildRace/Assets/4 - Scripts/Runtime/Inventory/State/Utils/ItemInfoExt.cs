namespace Game.Inventory
{
    public static class ItemInfoExt
    {
        public static bool IsNotExist(this ItemInfo item)
        {
            return item == null || item.IsRemoved.Value;
        }

        public static bool IsNotExist(this ItemSlotInfo slot)
        {
            return slot == null;
        }

        public static bool IsNotExist(this ItemsGridInfo grid)
        {
            return grid == null;
        }

        public static bool IsNotExist(this IPlacementContainer placement)
        {
            return placement == null;
        }
    }
}