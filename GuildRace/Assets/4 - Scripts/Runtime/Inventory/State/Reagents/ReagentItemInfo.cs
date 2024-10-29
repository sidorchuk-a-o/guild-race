﻿namespace Game.Inventory
{
    public class ReagentItemInfo : ItemInfo, IStackableItem
    {
        public ItemStackInfo Stack { get; }

        public ReagentItemInfo(string id, ReagentItemData data) : base(id, data)
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