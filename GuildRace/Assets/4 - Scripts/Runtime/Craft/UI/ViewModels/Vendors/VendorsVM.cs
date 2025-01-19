using AD.Services.Router;

namespace Game.Craft
{
    public class VendorsVM : VMCollection<VendorInfo, VendorVM>
    {
        public VendorsVM(IVendorsCollection values) : base(values)
        {
        }

        protected override VendorVM Create(VendorInfo value)
        {
            return new VendorVM(value);
        }
    }
}