#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using Cysharp.Threading.Tasks;

namespace Game.Inventory
{
    public class PlaceItemInSlot : ReleaseHandler
    {
        protected override bool CheckContext(ReleaseResult result)
        {
            return result.Context.SelectedSlot != null;
        }

        protected override async UniTask Process(ReleaseResult result)
        {
            var selectedItem = result.Context.PickupResult.SelectedItem;
            var selectedSlot = result.Context.SelectedSlot;

            result.Placed = selectedSlot.ViewModel.TryAddItem(selectedItem);
        }
    }
}