#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using Cysharp.Threading.Tasks;

namespace Game.Inventory
{
    public class PlaceItemInSlot : ReleaseHandler
    {
        protected override bool CheckContext(ReleaseResult result)
        {
            return result.Context.SelectedSlotVM != null;
        }

        protected override async UniTask Process(ReleaseResult result)
        {
            var selectedItemVM = result.Context.PickupResult.SelectedItemVM;
            var selectedSlotVM = result.Context.SelectedSlotVM;

            result.Placed = selectedSlotVM.TryAddItem(selectedItemVM);
        }
    }
}