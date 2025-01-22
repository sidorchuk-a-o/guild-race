namespace Game.Craft
{
    public interface ICraftService
    {
        IVendorsCollection Vendors { get; }
        RecycleSlotInfo RecycleSlot { get; }

        RecyclingResult GetRecyclingResult(string itemId);
        void StartCraftingProcess(StartCraftingEM craftingEM);
    }
}