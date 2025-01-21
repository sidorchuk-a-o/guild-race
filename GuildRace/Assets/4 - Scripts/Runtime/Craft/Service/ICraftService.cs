namespace Game.Craft
{
    public interface ICraftService
    {
        IVendorsCollection Vendors { get; }
        RemoveItemSlotInfo RemoveItemSlot { get; }

        void StartCraftingProcess(StartCraftingEM craftingEM);
    }
}