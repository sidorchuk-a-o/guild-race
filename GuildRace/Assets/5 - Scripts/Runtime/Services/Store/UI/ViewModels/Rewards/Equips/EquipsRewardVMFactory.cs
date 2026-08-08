using System;
using AD.Game.Inventory;
using AD.Router;
using AD.Store;
using VContainer;

namespace AD.Game.Store
{
    public class EquipsRewardVMFactory : VMFactory, IRewardsVMFactory
    {
        private InventoryVMFactory inventoryVMF;

        public Type DataType { get; } = typeof(EquipsReward);

        [Inject]
        public void Inject(InventoryVMFactory inventoryVMF)
        {
            this.inventoryVMF = inventoryVMF;
        }

        public RewardVM Create(RewardData data, StoreVMFactory storeVMF)
        {
            return new EquipsRewardVM(data as EquipsReward, storeVMF);
        }

        public RewardResultVM Create(RewardResult data, StoreVMFactory storeVMF)
        {
            return new EquipRewardResultVM(data as EquipRewardResult, storeVMF, inventoryVMF);
        }
    }
}