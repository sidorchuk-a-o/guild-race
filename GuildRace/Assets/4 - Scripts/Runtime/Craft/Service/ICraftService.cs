namespace Game.Craft
{
    public interface ICraftService
    {
        IVendorsCollection Vendors { get; }

        void StartCraftingProcess(StartCraftingEM craftingEM);
    }
}