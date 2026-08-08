using Cysharp.Threading.Tasks;

namespace AD.Game.Inventory
{
    public class PlaceItemInGrid : ReleaseHandler
    {
        protected override bool CheckContext(ReleaseResult result)
        {
            return result.Context.SelectedGridVM != null;
        }

        protected override UniTask Process(ReleaseResult result)
        {
            var selectedItemVM = result.Context.PickupResult.SelectedItemVM;
            var selectedGridVM = result.Context.SelectedGridVM;
            var positionOnGrid = result.Context.PositionOnSelectedGrid;

            result.Placed = selectedGridVM.TryPlaceItem(selectedItemVM, positionOnGrid.Item);

            return UniTask.CompletedTask;
        }
    }
}