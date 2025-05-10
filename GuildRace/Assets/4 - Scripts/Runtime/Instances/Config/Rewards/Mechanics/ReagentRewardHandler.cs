using System.Linq;
using AD.ToolsCollection;
using Game.Craft;
using Game.Guild;
using Game.Inventory;
using VContainer;

namespace Game.Instances
{
    public class ReagentRewardHandler : RewardHandler
    {
        private CraftConfig craftConfig;

        private IGuildService guildService;
        private IInventoryService inventoryService;

        [Inject]
        public void Inject(CraftConfig craftConfig, IGuildService guildService, IInventoryService inventoryService)
        {
            this.craftConfig = craftConfig;
            this.guildService = guildService;
            this.inventoryService = inventoryService;
        }

        public override void ApplyReward(InstanceRewardData reward)
        {
            var reagentCellTypes = craftConfig.ReagentsParams.GridParams.CellTypes;
            var reagentBank = guildService.BankTabs.FirstOrDefault(x =>
            {
                return reagentCellTypes.Contains(x.Grid.CellType);
            });

            if (reagentBank == null)
            {
                return;
            }

            var reagentId = reward.MechanicParams[0].IntParse();
            var reagentCount = reward.MechanicParams[1].IntParse();

            var reagentItem = inventoryService.Factory.CreateItem(reagentId) as ReagentItemInfo;

            reagentItem.Stack.SetValue(reagentCount);

            inventoryService.TryPlaceItem(new PlaceInPlacementArgs
            {
                PlacementId = reagentBank.Grid.Id,
                ItemId = reagentItem.Id
            });
        }
    }
}