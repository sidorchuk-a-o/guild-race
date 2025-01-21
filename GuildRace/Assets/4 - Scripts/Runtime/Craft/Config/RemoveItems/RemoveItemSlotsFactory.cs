using Game.Inventory;
using System;

namespace Game.Craft
{
    public class RemoveItemSlotsFactory : ItemSlotsFactory
    {
        public override Type DataType { get; } = typeof(RemoveItemSlotData);

        protected override ItemSlotInfo CreateInfo(string id, ItemSlotData data)
        {
            var removeSlotData = data as RemoveItemSlotData;

            return new RemoveItemSlotInfo(id, removeSlotData);
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