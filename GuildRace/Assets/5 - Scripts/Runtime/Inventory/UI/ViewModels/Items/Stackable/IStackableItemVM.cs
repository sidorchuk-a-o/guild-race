using AD.Game.UI;

namespace AD.Game.Inventory
{
    public interface IStackableItemVM
    {
        ItemStackVM StackVM { get; }
        UIStateVM StackableStateVM { get; }

        bool CheckPossibilityOfSplit();
        bool CheckPossibilityOfTransfer(ItemVM itemVM);
    }
}