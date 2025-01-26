using AD.Services.Localization;
using Game.Inventory;

namespace Game.Instances
{
    public class ConsumablesItemInfo : ItemInfo, IStackableItem
    {
        public ItemStackInfo Stack { get; }
        public Rarity Rarity { get; }
        public LocalizeKey DescKey { get; }

        public ConsumablesItemInfo(string id, ConsumablesItemData data) : base(id, data)
        {
            Stack = new(data.Stack);
            Rarity = data.Rarity;
            DescKey = data.DescKey;
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