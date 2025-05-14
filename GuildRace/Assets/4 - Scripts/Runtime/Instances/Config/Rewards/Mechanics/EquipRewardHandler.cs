using System.Collections.Generic;
using System.Linq;
using AD.ToolsCollection;
using Game.Guild;
using Game.Inventory;
using VContainer;

namespace Game.Instances
{
    public class EquipRewardHandler : RewardHandler
    {
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

        public override IEnumerable<RewardResult> ApplyRewards(IReadOnlyList<InstanceRewardData> rewards, CompleteResult result)
        {
            if (result != CompleteResult.Completed)
            {
                yield break;
            }

            var randomReward = rewards.RandomValue();
            var rewardResult = ApplyReward(randomReward, result);

            if (rewardResult == null)
            {
                yield break;
            }

            yield return rewardResult;
        }

        public override RewardResult ApplyReward(InstanceRewardData reward, CompleteResult result)
        {
            if (result != CompleteResult.Completed)
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

            var equipId = reward.MechanicParams[0].IntParse();
            var equipItem = inventoryService.Factory.CreateItem(equipId);

            inventoryService.TryPlaceItem(new PlaceInPlacementArgs
            {
                PlacementId = equipBank.Grid.Id,
                ItemId = equipItem.Id
            });

            return new EquipRewardResult
            {
                ItemId = equipItem.Id,
                ItemDataId = equipItem.DataId
            };
        }
    }
}