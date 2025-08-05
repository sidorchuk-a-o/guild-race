using System;
using System.Collections.Generic;
using System.Linq;
using AD.Services.Store;
using AD.ToolsCollection;
using Game.Guild;
using Game.Instances;
using Game.Inventory;
using UnityEngine;
using VContainer;
using RewardResult = AD.Services.Store.RewardResult;

namespace Game.Store
{
    [Handler(typeof(EquipsReward))]
    public class EquipsRewardHandler : RewardHandler<EquipsReward>
    {
        [SerializeField] private int mechanicId;

        private InstancesConfig instancesConfig;
        private InventoryConfig inventoryConfig;

        private IGuildService guildService;
        private IInventoryService inventoryService;

        [Inject]
        public void Inject(
            InstancesConfig instancesConfig,
            InventoryConfig inventoryConfig,
            IGuildService guildService,
            IInventoryService inventoryService)
        {
            this.instancesConfig = instancesConfig;
            this.inventoryConfig = inventoryConfig;
            this.guildService = guildService;
            this.inventoryService = inventoryService;
        }

        protected override IEnumerable<RewardResult> Apply(EquipsReward reward)
        {
            var instanceRewards = instancesConfig.GetInstanceRewards(reward.InstanceType);

            var rewards = instanceRewards
                .Where(x => x.MechanicId == mechanicId)
                .RandomValues(reward.Count);

            return rewards.Select(x => Apply(x, reward));
        }

        private EquipRewardResult Apply(InstanceRewardData instanceReward, EquipsReward reward)
        {
            var equipCellTypes = inventoryConfig.EquipsParams.GridParams.CellTypes;
            var equipBank = guildService.BankTabs.FirstOrDefault(x =>
            {
                return equipCellTypes.Contains(x.Grid.CellType);
            });

            if (equipBank == null)
            {
                return null;
            }

            var equipId = GetEquipId(instanceReward);
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
                RewardType = reward.GetType()
            };
        }

        public int GetEquipId(InstanceRewardData reward)
        {
            return reward.MechanicParams[0].IntParse();
        }
    }
}