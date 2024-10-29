using AD.Services.Router;
using System.Collections.Generic;

namespace Game.Inventory
{
    public interface IPlacementContainerVM : IViewModel
    {
        UIStateVM PlacementState { get; }

        IEnumerable<IPlacementContainerVM> GetPlacementsInChildren();

        bool CheckPossibilityOfPlacement(ItemVM itemVM);
        bool TryPlaceItem(ItemVM itemVM);
        bool TryRemoveItem(ItemVM itemVM);
    }
}