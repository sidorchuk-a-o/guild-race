using Game.Craft;
using Game.Inventory;

namespace Game.Craft
{
    public class RemoveItemSlotInfo : ItemSlotInfo
    {
        public RemoveItemSlotInfo(string id, RemoveItemSlotData data) : base(id, data)
        {
        }

        public override bool CheckPossibilityOfPlacement(ItemInfo itemInfo)
        {
            return itemInfo is not ReagentItemInfo;
        }

        public override bool TryAddItem(ItemInfo item)
        {
            return base.TryAddItem(item);
        }

        public override bool TryRemoveItem()
        {
            return base.TryRemoveItem();
        }
    }
}