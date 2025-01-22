using Game.Inventory;
using System;

namespace Game.Craft
{
    public class RecycleSlotsFactory : ItemSlotsFactory
    {
        public override Type DataType { get; } = typeof(RecycleSlotData);

        protected override ItemSlotInfo CreateInfo(string id, ItemSlotData data)
        {
            var recycleSlotData = data as RecycleSlotData;

            return new RecycleSlotInfo(id, recycleSlotData);
        }

        public override ItemSlotSM CreateSave(ItemSlotInfo info)
        {
            return null;
        }

        protected override ItemSlotInfo ReadSave(ItemSlotData data, ItemSlotSM save)
        {
            return null;
        }
    }
}