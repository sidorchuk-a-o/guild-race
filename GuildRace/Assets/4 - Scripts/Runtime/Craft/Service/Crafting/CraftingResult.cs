namespace Game.Craft
{
    public readonly struct CraftingResult
    {
        public int ItemDataId { get; }
        public int Count { get; }

        public CraftingResult(int itemDataId, int count)
        {
            ItemDataId = itemDataId;
            Count = count;
        }
    }
}