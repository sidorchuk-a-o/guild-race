using System;

namespace Game.Craft
{
    public interface ICraftService
    {
        IVendorsCollection Vendors { get; }
        RecycleSlotInfo RecycleSlot { get; }

        IObservable<CraftingResult> OnCraftingComplete { get; }

        RecyclingResult GetRecyclingResult(string itemId);
        void StartCraftingProcess(StartCraftingEM craftingEM);
    }
}