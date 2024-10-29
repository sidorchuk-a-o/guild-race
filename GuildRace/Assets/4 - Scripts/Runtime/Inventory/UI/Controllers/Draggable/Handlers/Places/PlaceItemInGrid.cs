#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using Cysharp.Threading.Tasks;

namespace Game.Inventory
{
    public class PlaceItemInGrid : ReleaseHandler
    {
        protected override bool CheckContext(ReleaseResult result)
        {
            return result.Context.SelectedGrid != null;
        }

        protected override async UniTask Process(ReleaseResult result)
        {
            var selectedItem = result.Context.PickupResult.SelectedItem;
            var selectedGrid = result.Context.SelectedGrid;
            var positionOnGrid = result.Context.PositionOnSelectedGrid;

            result.Placed = selectedGrid.ViewModel.TryPlaceItem(selectedItem, positionOnGrid.Item);
        }
    }
}