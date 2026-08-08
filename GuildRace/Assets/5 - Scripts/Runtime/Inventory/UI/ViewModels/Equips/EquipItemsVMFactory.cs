using System;
using AD.Router;

namespace AD.Game.Inventory
{
    public class EquipItemsVMFactory : VMFactory, IItemsVMFactory
    {
        public Type DataType { get; } = typeof(EquipItemData);

        public ItemVM Create(ItemInfo info, InventoryVMFactory inventoryVMF)
        {
            return new EquipItemVM(info as EquipItemInfo, inventoryVMF);
        }

        public ItemDataVM Create(ItemData data, InventoryVMFactory inventoryVMF)
        {
            return new EquipDataVM(data as EquipItemData, inventoryVMF);
        }
    }
}