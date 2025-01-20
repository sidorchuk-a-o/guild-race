using Game.Inventory;
using System;

namespace Game.Instances
{
    public class СonsumablesItemsVMFactory : ItemsVMFactory
    {
        public override Type InfoType { get; } = typeof(СonsumablesItemInfo);

        public override ItemVM Create(ItemInfo itemInfo, InventoryVMFactory inventoryVMF)
        {
            return new СonsumablesItemVM(itemInfo as СonsumablesItemInfo, inventoryVMF);
        }
    }
}