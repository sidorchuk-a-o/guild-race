using System;
using AD.Services.Store;
using Game.Inventory;
using VContainer;

namespace Game.Store
{
    public class EquipsRewardVMFactory : RewardsVMFactory
    {
        private InventoryVMFactory inventoryVMF;

        public override Type DataType { get; } = typeof(EquipsReward);

        [Inject]
        public void Inject(InventoryVMFactory inventoryVMF)
        {
            this.inventoryVMF = inventoryVMF;
        }

        public override RewardVM Create(RewardData data, StoreVMFactory storeVMF)
        {
            return new EquipsRewardVM(data as EquipsReward, storeVMF);
        }

        public override RewardResultVM Create(RewardResult data, StoreVMFactory storeVMF)
        {
            return new EquipRewardResultVM(data as EquipRewardResult, storeVMF, inventoryVMF);
        }
    }
}