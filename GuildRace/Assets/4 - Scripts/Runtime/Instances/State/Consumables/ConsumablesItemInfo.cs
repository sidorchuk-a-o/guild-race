using System.Collections.Generic;
using AD.Services.Localization;
using Game.Inventory;

namespace Game.Instances
{
    public class ConsumablesItemInfo : ItemInfo, IStackableItem
    {
        public LocalizeKey DescKey { get; }

        public ItemStackInfo Stack { get; }
        public Rarity Rarity { get; }
        public ConsumableType Type { get; }

        public int MechanicId { get; }
        public IReadOnlyList<string> MechanicParams { get; }

        public ConsumablesItemInfo(string id, ConsumablesItemData data, ItemType itemType) : base(id, data, itemType)
        {
            DescKey = data.DescKey;
            Stack = new(data.Stack);
            Rarity = data.Rarity;
            Type = data.Type;
            MechanicId = data.MechanicId;
            MechanicParams = data.MechanicParams;
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