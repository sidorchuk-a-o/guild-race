using Cysharp.Threading.Tasks;

namespace AD.Game.Inventory
{
    public class PlaceItemInsideOtherItem : ReleaseHandler
    {
        protected override bool CheckContext(ReleaseResult result)
        {
            var hoveringItemVM = result.Context.HoveredItemVM;

            return hoveringItemVM is IPlacementContainerVM;
        }

        protected override UniTask Process(ReleaseResult result)
        {
            var hoveringItemVM = result.Context.HoveredItemVM;
            var selectedItemVM = result.Context.PickupResult.SelectedItemVM;

            if (hoveringItemVM is IPlacementContainerVM placementVM)
            {
                result.Placed = placementVM.TryPlaceItem(selectedItemVM);
            }

            return UniTask.CompletedTask;
        }
    }
}