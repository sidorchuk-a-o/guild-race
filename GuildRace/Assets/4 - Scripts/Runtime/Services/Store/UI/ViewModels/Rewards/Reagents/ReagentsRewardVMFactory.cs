using System;
using AD.Services.Store;
using Game.Craft;
using Game.Inventory;
using VContainer;

namespace Game.Store
{
    public class ReagentsRewardVMFactory : RewardsVMFactory
    {
        private CraftVMFactory craftVMF;
        private InventoryVMFactory inventoryVMF;

        public override Type DataType { get; } = typeof(ReagentsReward);

        [Inject]
        public void Inject(CraftVMFactory craftVMF, InventoryVMFactory inventoryVMF)
        {
            this.craftVMF = craftVMF;
            this.inventoryVMF = inventoryVMF;
        }

        public override RewardVM Create(RewardData data, StoreVMFactory storeVMF)
        {
            return new ReagentsRewardVM(data as ReagentsReward, storeVMF, craftVMF);
        }

        public override RewardResultVM Create(RewardResult data, StoreVMFactory storeVMF)
        {
            return new ReagentRewardResultVM(data as ReagentRewardResult, storeVMF, inventoryVMF);
        }
    }
}