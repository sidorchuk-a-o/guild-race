using Game.Inventory;
using System;

namespace Game.Instances
{
    public class ConsumablesItemsVMFactory : ItemsVMFactory
    {
        public override Type InfoType { get; } = typeof(ConsumablesItemInfo);

        public override ItemVM Create(ItemInfo itemInfo, InventoryVMFactory inventoryVMF)
        {
            return new ConsumablesItemVM(itemInfo as ConsumablesItemInfo, inventoryVMF);
        }
    }
}