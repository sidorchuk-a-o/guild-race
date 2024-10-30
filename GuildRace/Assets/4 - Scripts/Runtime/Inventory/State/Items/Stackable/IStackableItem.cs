namespace Game.Inventory
{
    public interface IStackableItem
    {
        ItemStackInfo Stack { get; }

        bool CheckPossibilityOfSplit();
        bool CheckPossibilityOfTransfer(ItemInfo item);
    }
}