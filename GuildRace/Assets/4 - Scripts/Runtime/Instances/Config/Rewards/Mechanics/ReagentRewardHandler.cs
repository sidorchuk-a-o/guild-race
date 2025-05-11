using System.Collections.Generic;
using System.Linq;
using AD.ToolsCollection;
using Game.Craft;
using Game.Guild;
using Game.Inventory;
using UnityEngine;
using VContainer;

namespace Game.Instances
{
    public class ReagentRewardHandler : RewardHandler
    {
        [SerializeField] private float failedMod = 0.5f;

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

        public override void ApplyRewards(IReadOnlyList<InstanceRewardData> rewards, CompleteResult result)
        {
            rewards.ForEach(reward => ApplyReward(reward, result));
        }

        public override void ApplyReward(InstanceRewardData reward, CompleteResult result)
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

            if (result != CompleteResult.Completed)
            {
                reagentCount = Mathf.RoundToInt(reagentCount * failedMod);
            }

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