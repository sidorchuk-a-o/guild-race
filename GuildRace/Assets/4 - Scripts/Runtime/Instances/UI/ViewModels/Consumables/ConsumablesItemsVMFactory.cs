using Game.Inventory;
using System;
using VContainer;

namespace Game.Instances
{
    public class ConsumablesItemsVMFactory : ItemsVMFactory
    {
        private InstancesVMFactory instancesVMF;

        public override Type InfoType { get; } = typeof(ConsumablesItemInfo);

        [Inject]
        public void Inject(InstancesVMFactory instancesVMF)
        {
            this.instancesVMF = instancesVMF;
        }

        public override ItemVM Create(ItemInfo itemInfo, InventoryVMFactory inventoryVMF)
        {
            return new ConsumablesItemVM(itemInfo as ConsumablesItemInfo, instancesVMF);
        }
    }
}