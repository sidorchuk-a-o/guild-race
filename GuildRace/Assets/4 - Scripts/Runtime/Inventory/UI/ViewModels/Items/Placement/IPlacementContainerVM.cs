using AD.Services.Router;
using Game.UI;
using System.Collections.Generic;

namespace Game.Inventory
{
    public interface IPlacementContainerVM : IViewModel
    {
        UIStateVM PlacementStateVM { get; }

        IEnumerable<IPlacementContainerVM> GetPlacementsInChildren();

        bool CheckPossibilityOfPlacement(ItemVM itemVM);
        bool TryPlaceItem(ItemVM itemVM);
        bool TryRemoveItem(ItemVM itemVM);
    }
}