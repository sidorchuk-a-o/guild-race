using System;
using UniRx;

namespace Game.Craft
{
    public interface ICraftService
    {
        IVendorsCollection Vendors { get; }
        RecycleSlotInfo RecycleSlot { get; }

        IReadOnlyReactiveProperty<float> PriceDiscount { get; }
        IObservable<CraftingResult> OnCraftingComplete { get; }

        bool CreateCraftOrder(CraftOrderArgs args);

        RecyclingResult GetRecyclingResult(string itemId);
        void StartCraftingProcess(StartCraftingEM craftingEM);
    }
}