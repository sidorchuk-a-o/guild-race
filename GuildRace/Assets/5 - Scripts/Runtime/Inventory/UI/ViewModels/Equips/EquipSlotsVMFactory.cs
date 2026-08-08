using System;
using AD.Router;

namespace AD.Game.Inventory
{
    public class EquipSlotsVMFactory : VMFactory, IItemSlotsVMFactory
    {
        public Type InfoType { get; } = typeof(EquipSlotInfo);

        public ItemSlotVM Create(ItemSlotInfo info, InventoryVMFactory inventoryVMF)
        {
            return new EquipSlotVM(info as EquipSlotInfo, inventoryVMF);
        }
    }
}