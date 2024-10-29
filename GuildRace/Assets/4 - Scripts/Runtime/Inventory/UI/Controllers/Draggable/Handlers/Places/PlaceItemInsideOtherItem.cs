#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using Cysharp.Threading.Tasks;

namespace Game.Inventory
{
    public class PlaceItemInsideOtherItem : ReleaseHandler
    {
        protected override bool CheckContext(ReleaseResult result)
        {
            var hoveringItem = result.Context.HoveredItem;

            return hoveringItem != null
                && hoveringItem is IPlacementContainerVM;
        }

        protected override async UniTask Process(ReleaseResult result)
        {
            var hoveringItem = result.Context.HoveredItem;

            if (hoveringItem is IPlacementContainerVM placement)
            {
                var selectedItem = result.Context.PickupResult.SelectedItem;

                result.Placed = placement.TryPlaceItem(selectedItem);
            }
        }
    }
}