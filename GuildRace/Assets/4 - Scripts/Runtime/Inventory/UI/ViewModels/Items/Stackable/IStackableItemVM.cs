using AD.Services.Router;

namespace Game.Inventory
{
    public interface IStackableItemVM : IViewModel
    {
        ItemStackVM Stack { get; }
        UIStateVM StackableState { get; }

        bool CheckPossibilityOfSplit();
        bool CheckPossibilityOfTransfer(ItemVM itemVM);
    }
}