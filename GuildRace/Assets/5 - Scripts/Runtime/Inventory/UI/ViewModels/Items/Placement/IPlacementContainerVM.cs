using System.Collections.Generic;
using AD.Game.UI;

namespace AD.Game.Inventory
{
    public interface IPlacementContainerVM
    {
        UIStateVM PlacementStateVM { get; }

        IEnumerable<IPlacementContainerVM> GetPlacementsInChildren();

        bool CheckPossibilityOfPlacement(ItemVM itemVM);
        bool TryPlaceItem(ItemVM itemVM);
        bool TryRemoveItem(ItemVM itemVM);
    }
}