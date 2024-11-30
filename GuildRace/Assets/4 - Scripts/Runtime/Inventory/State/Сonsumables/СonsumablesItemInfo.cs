namespace Game.Inventory
{
    public class СonsumablesItemInfo : ItemInfo, IStackableItem
    {
        public ItemStackInfo Stack { get; }

        public СonsumablesItemInfo(string id, СonsumablesItemData data) : base(id, data)
        {
            Stack = new(data.Stack);
        }

        // == IStackableItem ==

        public bool CheckPossibilityOfSplit()
        {
            return Stack.Value > 1;
        }

        public bool CheckPossibilityOfTransfer(ItemInfo item)
        {
            return !Stack.IsFulled && DataId == item.DataId;
        }
    }
}