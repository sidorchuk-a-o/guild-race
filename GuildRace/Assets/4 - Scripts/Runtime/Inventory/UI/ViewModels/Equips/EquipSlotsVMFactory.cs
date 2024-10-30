using System;

namespace Game.Inventory
{
    public class EquipSlotsVMFactory : ItemSlotsVMFactory
    {
        public override Type InfoType { get; } = typeof(EquipSlotInfo);

        public override ItemSlotVM Create(ItemSlotInfo info, InventoryVMFactory inventoryVMF)
        {
            return new EquipSlotVM(info as EquipSlotInfo, inventoryVMF);
        }
    }
}