using System;
using Game.Inventory;

namespace Game.Craft
{
    public class ReagentItemsVMFactory : ItemsVMFactory
    {
        public override Type DataType { get; } = typeof(ReagentItemData);

        public override ItemVM Create(ItemInfo itemInfo, InventoryVMFactory inventoryVMF)
        {
            return new ReagentItemVM(itemInfo as ReagentItemInfo, inventoryVMF);
        }

        public override ItemDataVM Create(ItemData data, InventoryVMFactory inventoryVMF)
        {
            return new ReagentDataVM(data as ReagentItemData, inventoryVMF);
        }
    }
}