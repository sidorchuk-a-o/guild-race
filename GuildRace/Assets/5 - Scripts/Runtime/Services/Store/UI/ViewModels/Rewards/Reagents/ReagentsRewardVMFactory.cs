using System;
using AD.Game.Craft;
using AD.Game.Inventory;
using AD.Router;
using AD.Store;
using VContainer;

namespace AD.Game.Store
{
    public class ReagentsRewardVMFactory : VMFactory, IRewardsVMFactory
    {
        private CraftVMFactory craftVMF;
        private InventoryVMFactory inventoryVMF;

        public Type DataType { get; } = typeof(ReagentsReward);

        [Inject]
        public void Inject(CraftVMFactory craftVMF, InventoryVMFactory inventoryVMF)
        {
            this.craftVMF = craftVMF;
            this.inventoryVMF = inventoryVMF;
        }

        public RewardVM Create(RewardData data, StoreVMFactory storeVMF)
        {
            return new ReagentsRewardVM(data as ReagentsReward, storeVMF, craftVMF);
        }

        public RewardResultVM Create(RewardResult data, StoreVMFactory storeVMF)
        {
            return new ReagentRewardResultVM(data as ReagentRewardResult, storeVMF, inventoryVMF);
        }
    }
}