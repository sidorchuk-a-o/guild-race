using AD.Router;

namespace AD.Game.Craft
{
    public class VendorsVM : VMCollection<VendorInfo, VendorVM>
    {
        private readonly CraftVMFactory craftVMF;

        public VendorsVM(IVendorsCollection values, CraftVMFactory craftVMF) : base(values)
        {
            this.craftVMF = craftVMF;
        }

        protected override VendorVM Create(VendorInfo value)
        {
            return new(value, craftVMF);
        }
    }
}