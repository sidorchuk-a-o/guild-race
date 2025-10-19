using System.Linq;
using System.Collections.Generic;
using AD.Services;
using AD.Services.Save;
using Game.Guild;
using Game.GuildLevels;
using Game.Inventory;
using VContainer;
using UniRx;

namespace Game.Craft
{
    public class CraftState : ServiceState<CraftConfig, CraftStateSM>
    {
        private readonly IGuildService guildService;
        private readonly IGuildLevelsService guildLevelsService;
        private readonly IInventoryService inventoryService;

        private readonly VendorsCollection vendors = new(null);
        private readonly ReactiveProperty<float> priceDiscount = new();

        private readonly CraftLevelContext levelContext = new();

        public override string SaveKey => CraftStateSM.key;
        public override SaveSource SaveSource => SaveSource.app;

        public IVendorsCollection Vendors => vendors;
        public RecycleSlotInfo RecycleSlot { get; }
        public IReadOnlyReactiveProperty<float> PriceDiscount => priceDiscount;

        public CraftState(
            CraftConfig config,
            IGuildService guildService,
            IGuildLevelsService guildLevelsService,
            IInventoryService inventoryService,
            IObjectResolver resolver)
            : base(config, resolver)
        {
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
            return new CraftStateSM();
        }

        protected override void ReadSave(CraftStateSM save)
        {
            if (save == null)
            {
                vendors.AddRange(GetDefaultVendors());

                CreateDefaultReagents();

                return;
            }

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
                .FirstOrDefault(x => reagentCellTypes.Contains(x.CellType));

            var reagents = reagentsParams.Items
                .Select(x => inventoryService.Factory.CreateItem(x))
                .OfType<ReagentItemInfo>();

            foreach (var reagent in reagents)
            {
                reagent.Stack.SetValue(20);

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