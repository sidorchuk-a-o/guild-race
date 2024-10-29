using System.Collections.Generic;

namespace Game.Inventory
{
    public interface IPlacementContainer
    {
        IEnumerable<ItemInfo> GetItems();

        bool CheckPossibilityOfPlacement(ItemInfo item);
        bool TryPlaceItem(ItemInfo item);
        bool TryRemoveItem(ItemInfo item);
    }
}