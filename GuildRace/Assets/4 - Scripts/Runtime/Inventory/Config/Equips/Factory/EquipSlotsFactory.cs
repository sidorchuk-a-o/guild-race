using System;

namespace Game.Inventory
{
    public class EquipSlotsFactory : ItemSlotsFactory
    {
        public override Type DataType { get; } = typeof(EquipSlotData);

        protected override ItemSlotInfo CreateInfo(string id, ItemSlotData data)
        {
            var equipSlotData = data as EquipSlotData;

            return new EquipSlotInfo(id, equipSlotData);
        }

        public override ItemSlotSM CreateSave(ItemSlotInfo info)
        {
            var equipSlot = info as EquipSlotInfo;

            return new EquipSlotSM(equipSlot, InventoryFactory);
        }

        protected override ItemSlotInfo ReadSave(ItemSlotData data, ItemSlotSM save)
        {
            var equipSlotData = data as EquipSlotData;
            var equipSlotSave = save as EquipSlotSM;

            return equipSlotSave.GetValue(equipSlotData, InventoryFactory);
        }
    }
}