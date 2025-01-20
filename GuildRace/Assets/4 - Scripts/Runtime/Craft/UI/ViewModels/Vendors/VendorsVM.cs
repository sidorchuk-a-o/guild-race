using AD.Services.Router;
using Game.Inventory;

namespace Game.Craft
{
    public class VendorsVM : VMCollection<VendorInfo, VendorVM>
    {
        private readonly InventoryVMFactory inventoryVMF;

        public VendorsVM(IVendorsCollection values, InventoryVMFactory inventoryVMF) : base(values)
        {
            this.inventoryVMF = inventoryVMF;
        }

        protected override VendorVM Create(VendorInfo value)
        {
            return new VendorVM(value, inventoryVMF);
        }
    }
}