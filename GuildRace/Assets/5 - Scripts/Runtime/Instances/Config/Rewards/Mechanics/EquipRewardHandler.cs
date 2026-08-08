using System.Collections.Generic;
using System.Linq;
using AD.Game.Guild;
using AD.Game.Inventory;
using AD.ToolsCollection;
using UnityEngine;
using VContainer;

namespace AD.Game.Instances
{
    public class EquipRewardHandler : RewardHandler
    {
        [SerializeField] private List<EquipRewardParams> parameters;

        private InventoryConfig inventoryConfig;

        private IGuildService guildService;
        private IInventoryService inventoryService;

        [Inject]
        public void Inject(InventoryConfig inventoryConfig, IGuildService guildService, IInventoryService inventoryService)
        {
            this.inventoryConfig = inventoryConfig;
            this.guildService = guildService;
            this.inventoryService = inventoryService;
        }

        public static int GetEquipId(InstanceRewardData reward)
        {
            return reward.MechanicParams[0].IntParse();
        }

        public override IEnumerable<RewardResult> ApplyRewards(IReadOnlyList<InstanceRewardData> rewards, ActiveInstanceInfo instance)
        {
            if (instance.Result.CurrentValue != CompleteResult.Completed)
            {
                yield break;
            }

            var rewardsParams = parameters.First(x => x.InstanceType == instance.Instance.Type);

            var guaranteedRewards = rewards
                .RandomValues(rewardsParams.GuaranteedCount);

            var chanceRewards = rewards
                .RandomValues(rewardsParams.ChanceCount)
                .Where(_ => RandUtils.CheckChance(rewardsParams.Chance));

            var filteredRewards = guaranteedRewards.Concat(chanceRewards)
                .Select(x => ApplyReward(x, instance))
                .Where(x => x != null);

            foreach (var reward in filteredRewards)
            {
                yield return reward;
            }
        }

        public override RewardResult ApplyReward(InstanceRewardData reward, ActiveInstanceInfo instance)
        {
            if (instance.Result.CurrentValue != CompleteResult.Completed)
            {
                return null;
            }

            var equipCellTypes = inventoryConfig.EquipsParams.GridParams.CellTypes;
            var equipBank = guildService.BankTabs.FirstOrDefault(x =>
            {
                return equipCellTypes.Contains(x.Grid.CellType);
            });

            if (equipBank == null)
            {
                return null;
            }

            var equipId = GetEquipId(reward);
            var equipItem = inventoryService.Factory.CreateItem(equipId);

            inventoryService.TryPlaceItem(new PlaceInPlacementArgs
            {
                PlacementId = equipBank.Grid.Id,
                ItemId = equipItem.Id
            });

            return new EquipRewardResult
            {
                ItemId = equipItem.Id,
                ItemDataId = equipItem.DataId,
                RewardId = reward.Id
            };
        }
    }
}