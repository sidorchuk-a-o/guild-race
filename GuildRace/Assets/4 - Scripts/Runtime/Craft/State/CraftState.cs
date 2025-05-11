using AD.Services;
using AD.Services.Save;
using Game.Guild;
using Game.Inventory;
using System.Collections.Generic;
using System.Linq;
using VContainer;

namespace Game.Craft
{
    public class CraftState : ServiceState<CraftConfig, CraftStateSM>
    {
        private readonly IGuildService guildService;
        private readonly IInventoryService inventoryService;

        private readonly VendorsCollection vendors = new(null);

        public override string SaveKey => CraftStateSM.key;
        public override SaveSource SaveSource => SaveSource.app;

        public IVendorsCollection Vendors => vendors;
        public RecycleSlotInfo RecycleSlot { get; }

        public CraftState(
            CraftConfig config,
            IGuildService guildService,
            IInventoryService inventoryService,
            IObjectResolver resolver)
            : base(config, resolver)
        {
            this.guildService = guildService;
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

                //CreateDefaultReagents();

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

        private void CreateDefaultReagents()
        {
            var reagentsParams = config.ReagentsParams;
            var reagentCellTypes = reagentsParams.GridParams.CellTypes;

            var reagentsBank = guildService.BankTabs
                .Select(x => x.Grid)
                .FirstOrDefault(x => reagentCellTypes.Contains(x.CellType));

            var reagents = reagentsParams.Items
                .Select(x => inventoryService.Factory.CreateItem(x))
                .OfType<ReagentItemInfo>();

            foreach (var reagent in reagents)
            {
                reagent.Stack.SetValue(50);

                var placementArgs = new PlaceInPlacementArgs
                {
                    ItemId = reagent.Id,
                    PlacementId = reagentsBank.Id
                };

                inventoryService.TryPlaceItem(placementArgs);
            }
        }
    }
}