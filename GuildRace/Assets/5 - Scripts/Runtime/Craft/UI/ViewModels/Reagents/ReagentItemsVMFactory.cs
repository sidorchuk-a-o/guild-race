using System;
using AD.Game.Inventory;
using AD.Router;

namespace AD.Game.Craft
{
    public class ReagentItemsVMFactory : VMFactory, IItemsVMFactory
    {
        public Type DataType { get; } = typeof(ReagentItemData);

        public ItemVM Create(ItemInfo itemInfo, InventoryVMFactory inventoryVMF)
        {
            return new ReagentItemVM(itemInfo as ReagentItemInfo, inventoryVMF);
        }

        public ItemDataVM Create(ItemData data, InventoryVMFactory inventoryVMF)
        {
            return new ReagentDataVM(data as ReagentItemData, inventoryVMF);
        }
    }
}