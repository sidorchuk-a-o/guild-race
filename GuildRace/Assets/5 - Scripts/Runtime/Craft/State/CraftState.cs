using System.Collections.Generic;
using System.Linq;
using AD.Game.Guild;
using AD.Game.GuildLevels;
using AD.Game.Inventory;
using AD.Management;
using AD.Save;
using R3;

namespace AD.Game.Craft
{
    public class CraftState : ServiceState<CraftStateSM>
    {
        private readonly CraftConfig config;
        private readonly IGuildService guildService;
        private readonly IGuildLevelsService guildLevelsService;
        private readonly IInventoryService inventoryService;

        private readonly VendorsCollection vendors = new(null);
        private readonly ReactiveProperty<float> priceDiscount = new();

        private readonly CraftLevelContext levelContext = new();

        public override string SaveKey => "craft";
        public override SaveSource SaveSource => SaveSource.App;

        public IVendorsCollection Vendors => vendors;
        public RecycleSlotInfo RecycleSlot { get; }
        public ReadOnlyReactiveProperty<float> PriceDiscount => priceDiscount;

        public CraftState(
            CraftConfig config,
            IGuildService guildService,
            IGuildLevelsService guildLevelsService,
            IInventoryService inventoryService)
        {
            this.config = config;
            this.guildService = guildService;
            this.guildLevelsService = guildLevelsService;
            this.inventoryService = inventoryService;

            RecycleSlot = CreateRecycleSlot();
        }

        private RecycleSlotInfo CreateRecycleSlot()
        {
            var slotData = config.RecyclingParams.RecycleSlot;
            var slotInfo = inventoryService.Factory.CreateSlot(slotData);

            return slotInfo as RecycleSlotInfo;
        }

        public override void Init()
        {
            base.Init();

            guildLevelsService.RegisterContext(levelContext);

            levelContext.Discount.Subscribe(UpgradeDiscountCallback);
        }

        // == Guild Levels ==

        private void UpgradeDiscountCallback(float discount)
        {
            priceDiscount.Value = discount;
        }

        // == Save ==

        protected override CraftStateSM CreateSave()
        {
            return new();
        }

        protected override void CreateDefaultState()
        {
            vendors.AddRange(GetDefaultVendors());

            //CreateDefaultReagents();
        }

        protected override void ReadSave(CraftStateSM save)
        {
            vendors.AddRange(GetDefaultVendors());
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
                .First(x => reagentCellTypes.Contains(x.CellType));

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