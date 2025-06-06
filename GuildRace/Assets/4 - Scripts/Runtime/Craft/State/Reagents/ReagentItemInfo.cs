using Game.Inventory;

namespace Game.Craft
{
    public class ReagentItemInfo : ItemInfo, IStackableItem
    {
        public ItemStackInfo Stack { get; }

        public ReagentItemInfo(string id, ReagentItemData data, ItemType itemType) : base(id, data, itemType)
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