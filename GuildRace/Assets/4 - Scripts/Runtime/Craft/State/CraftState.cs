using AD.Services;
using AD.Services.Save;
using System.Collections.Generic;
using System.Linq;
using VContainer;

namespace Game.Craft
{
    public class CraftState : ServiceState<CraftConfig, CraftStateSM>
    {
        private readonly VendorsCollection vendors = new(null);

        public override string SaveKey => CraftStateSM.key;
        public override SaveSource SaveSource => SaveSource.app;

        public IVendorsCollection Vendors => vendors;

        public CraftState(CraftConfig config, IObjectResolver resolver) : base(config, resolver)
        {
        }

        // == Save ==

        protected override CraftStateSM CreateSave()
        {
            return new CraftStateSM
            {
                Vendors = Vendors
            };
        }

        protected override void ReadSave(CraftStateSM save)
        {
            if (save == null)
            {
                vendors.AddRange(GetDefaultVendors());

                return;
            }

            vendors.AddRange(save.GetVendors(config));
        }

        private IEnumerable<VendorInfo> GetDefaultVendors()
        {
            return config.Vendors.Select(data =>
            {
                return new VendorInfo(data);
            });
        }
    }
}