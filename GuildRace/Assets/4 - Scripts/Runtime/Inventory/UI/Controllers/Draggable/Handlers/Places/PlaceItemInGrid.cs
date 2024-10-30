#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using Cysharp.Threading.Tasks;

namespace Game.Inventory
{
    public class PlaceItemInGrid : ReleaseHandler
    {
        protected override bool CheckContext(ReleaseResult result)
        {
            return result.Context.SelectedGridVM != null;
        }

        protected override async UniTask Process(ReleaseResult result)
        {
            var selectedItemVM = result.Context.PickupResult.SelectedItemVM;
            var selectedGridVM = result.Context.SelectedGridVM;
            var positionOnGrid = result.Context.PositionOnSelectedGrid;

            result.Placed = selectedGridVM.TryPlaceItem(selectedItemVM, positionOnGrid.Item);
        }
    }
}