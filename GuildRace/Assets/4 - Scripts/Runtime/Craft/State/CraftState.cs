using AD.Services;
using AD.Services.Save;
using Game.Inventory;
using System.Collections.Generic;
using System.Linq;
using VContainer;

namespace Game.Craft
{
    public class CraftState : ServiceState<CraftConfig, CraftStateSM>
    {
        private readonly IInventoryService inventoryService;

        private readonly VendorsCollection vendors = new(null);

        public override string SaveKey => CraftStateSM.key;
        public override SaveSource SaveSource => SaveSource.app;

        public IVendorsCollection Vendors => vendors;
        public RecycleSlotInfo RecycleSlot { get; }

        public CraftState(
            CraftConfig config,
            IInventoryService inventoryService,
            IObjectResolver resolver)
            : base(config, resolver)
        {
            this.inventoryService = inventoryService;

            RecycleSlot = CreateRecycleSlot();
        }

        private RecycleSlotInfo CreateRecycleSlot()
        {
            var slotData = config.RecyclingParams.RecycleSlot;
            var slotInfo = inventoryService.Factory.CreateSlot(slotData);

            return slotInfo as RecycleSlotInfo;
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