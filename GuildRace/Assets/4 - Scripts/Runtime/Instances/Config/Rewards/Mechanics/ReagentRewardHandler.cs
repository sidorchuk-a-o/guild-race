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

        public int GetReagentId(InstanceRewardData reward)
        {
            return reward.MechanicParams[0].IntParse();
        }

        public int GetReagentCount(InstanceRewardData reward)
        {
            return reward.MechanicParams[1].IntParse();
        }

        public override IEnumerable<RewardResult> ApplyRewards(IReadOnlyList<InstanceRewardData> rewards, ActiveInstanceInfo instance)
        {
            return rewards
                .Select(reward => ApplyReward(reward, instance))
                .OfType<RewardResult>();
        }

        public override RewardResult ApplyReward(InstanceRewardData reward, ActiveInstanceInfo instance)
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

            var reagentId = GetReagentId(reward);
            var totalReagentCount = GetReagentCount(reward);

            if (instance.Result.Value != CompleteResult.Completed)
            {
                totalReagentCount = Mathf.RoundToInt(totalReagentCount * failedMod);
            }

            var reagentData = inventoryConfig.GetItem(reagentId) as IStackable;
            var itemsCount = Mathf.CeilToInt(totalReagentCount / (float)reagentData.Stack.Size);
            var itemIds = new List<string>(itemsCount);
            var rewardCount = totalReagentCount;

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
                Count = rewardCount,
                RewardId = reward.Id
            };
        }
    }
}