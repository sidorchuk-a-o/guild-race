using AD.Services.Router;
using Game.Inventory;

namespace Game.Instances
{
    public class ActiveInstancesVM : VMCollection<ActiveInstanceInfo, ActiveInstanceVM>
    {
        private readonly InstancesVMFactory instancesVMF;
        private readonly InventoryVMFactory inventoryVMF;

        public ActiveInstancesVM(
            IActiveInstancesCollection values,
            InstancesVMFactory instancesVMF,
            InventoryVMFactory inventoryVMF)
            : base(values)
        {
            this.instancesVMF = instancesVMF;
            this.inventoryVMF = inventoryVMF;
        }

        protected override ActiveInstanceVM Create(ActiveInstanceInfo value)
        {
            return new ActiveInstanceVM(value, instancesVMF, inventoryVMF);
        }
    }
}