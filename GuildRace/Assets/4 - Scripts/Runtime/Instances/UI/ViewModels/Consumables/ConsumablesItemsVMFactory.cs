using System;
using Game.Inventory;
using VContainer;

namespace Game.Instances
{
    public class ConsumablesItemsVMFactory : ItemsVMFactory
    {
        private InstancesVMFactory instancesVMF;

        public override Type DataType { get; } = typeof(ConsumablesItemData);

        [Inject]
        public void Inject(InstancesVMFactory instancesVMF)
        {
            this.instancesVMF = instancesVMF;
        }

        public override ItemVM Create(ItemInfo itemInfo, InventoryVMFactory inventoryVMF)
        {
            return new ConsumablesItemVM(itemInfo as ConsumablesItemInfo, instancesVMF);
        }

        public override ItemDataVM Create(ItemData data, InventoryVMFactory inventoryVMF)
        {
            return new ConsumablesDataVM(data as ConsumablesItemData, instancesVMF);
        }
    }
}