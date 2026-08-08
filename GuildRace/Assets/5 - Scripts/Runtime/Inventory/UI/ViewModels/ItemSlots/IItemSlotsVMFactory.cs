using System;

namespace AD.Game.Inventory
{
    public interface IItemSlotsVMFactory
    {
        Type InfoType { get; }

        ItemSlotVM Create(ItemSlotInfo info, InventoryVMFactory inventoryVMF);
    }
}