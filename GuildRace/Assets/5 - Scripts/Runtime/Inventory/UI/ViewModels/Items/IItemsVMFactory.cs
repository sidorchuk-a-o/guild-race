using System;

namespace AD.Game.Inventory
{
    public interface IItemsVMFactory
    {
        Type DataType { get; }

        ItemVM Create(ItemInfo info, InventoryVMFactory inventoryVMF);
        ItemDataVM Create(ItemData data, InventoryVMFactory inventoryVMF);
    }
}