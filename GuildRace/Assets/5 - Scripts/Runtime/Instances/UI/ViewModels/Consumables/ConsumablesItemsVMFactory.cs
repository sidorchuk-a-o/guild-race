using System;
using AD.Game.Inventory;
using AD.Router;
using VContainer;

namespace AD.Game.Instances
{
    public class ConsumablesItemsVMFactory : VMFactory, IItemsVMFactory
    {
        private InstancesVMFactory instancesVMF;

        public Type DataType { get; } = typeof(ConsumablesItemData);

        [Inject]
        public void Inject(InstancesVMFactory instancesVMF)
        {
            this.instancesVMF = instancesVMF;
        }

        public ItemVM Create(ItemInfo itemInfo, InventoryVMFactory inventoryVMF)
        {
            return new ConsumablesItemVM(itemInfo as ConsumablesItemInfo, instancesVMF);
        }

        public ItemDataVM Create(ItemData data, InventoryVMFactory inventoryVMF)
        {
            return new ConsumablesDataVM(data as ConsumablesItemData, instancesVMF);
        }
    }
}