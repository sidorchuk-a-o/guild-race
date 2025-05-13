using Game.Inventory;

namespace Game.Craft
{
    public class RecycleSlotInfo : ItemSlotInfo
    {
        public RecycleSlotInfo(string id, RecycleSlotData data) : base(id, data)
        {
        }

        public override bool CheckPossibilityOfPlacement(ItemInfo itemInfo)
        {
            return true;
        }
    }
}