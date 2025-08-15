using System.Collections.Generic;
using System.Linq;
using AD.Services.Store;
using Game.Craft;
using Game.Guild;
using Game.Inventory;
using UnityEngine;
using VContainer;

namespace Game.Store
{
    [Handler(typeof(ReagentsReward))]
    public class ReagentsRewardHandler : RewardHandler<ReagentsReward>
    {
        private CraftConfig craftConfig;
        private InventoryConfig inventoryConfig;

        private IGuildService guildService;
        private IInventoryService inventoryService;

        [Inject]
        public void Inject(CraftConfig craftConfig, InventoryConfig inventoryConfig, IGuildService guildService, IInventoryService inventoryService)
        {
            this.craftConfig = craftConfig;
            this.inventoryConfig = inventoryConfig;
            this.guildService = guildService;
            this.inventoryService = inventoryService;
        }

        protected override IEnumerable<RewardResult> Apply(ReagentsReward reward)
        {
            return reward.ReagentRewards.Select(x => Apply(x, reward));
        }

        private ReagentRewardResult Apply(ReagentRewardData reagentReward, ReagentsReward reward)
        {
            var reagentCellTypes = craftConfig.ReagentsParams.GridParams.CellTypes;
            var reagentBank = guildService.BankTabs.FirstOrDefault(x =>
            {
                return reagentCellTypes.Contains(x.Grid.CellType);
            });

            if (reagentBank == null)
            {
                return null;
            }

            var reagentId = reagentReward.ReagentId;
            var totalReagentCount = reagentReward.Amount;

            var reagentData = inventoryConfig.GetItem(reagentId) as IStackable;
            var itemsCount = Mathf.CeilToInt(totalReagentCount / (float)reagentData.Stack.Size);
            var itemIds = new List<string>(itemsCount);

            for (var i = 0; i < itemsCount; i++)
            {
                var reagentItem = inventoryService.Factory.CreateItem(reagentId) as ReagentItemInfo;
                var reagentCount = Mathf.Min(totalReagentCount, reagentItem.Stack.Size);

                reagentItem.Stack.SetValue(reagentCount);
                totalReagentCount -= reagentCount;

                itemIds.Add(reagentItem.Id);

                inventoryService.TryPlaceItem(new PlaceInPlacementArgs
                {
                    PlacementId = reagentBank.Grid.Id,
                    ItemId = reagentItem.Id
                });
            }

            return new ReagentRewardResult
            {
                ItemIds = itemIds,
                ItemDataId = reagentId,
                Count = reagentReward.Amount,
                RewardType = reward.GetType()
            };
        }
    }
}