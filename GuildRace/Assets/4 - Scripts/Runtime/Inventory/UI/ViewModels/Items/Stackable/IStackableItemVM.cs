using AD.Services.Router;
using Game.UI;

namespace Game.Inventory
{
    public interface IStackableItemVM : IViewModel
    {
        ItemStackVM StackVM { get; }
        UIStateVM StackableStateVM { get; }

        bool CheckPossibilityOfSplit();
        bool CheckPossibilityOfTransfer(ItemVM itemVM);
    }
}