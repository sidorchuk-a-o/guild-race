using AD.ToolsCollection;
using R3;

namespace AD.Game.Craft
{
    public interface ICraftService
    {
        IVendorsCollection Vendors { get; }
        RecycleSlotInfo RecycleSlot { get; }

        ReadOnlyReactiveProperty<float> PriceDiscount { get; }
        ReadOnlyReactiveEvent<CraftingResult> OnCraftingComplete { get; }

        bool CreateCraftOrder(CraftOrderArgs args);

        RecyclingResult GetRecyclingResult(string itemId);
        void StartCraftingProcess(StartCraftingEM craftingEM);
    }
}