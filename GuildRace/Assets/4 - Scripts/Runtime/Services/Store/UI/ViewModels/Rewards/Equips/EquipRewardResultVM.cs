using AD.Services.Router;
using AD.Services.Store;
using Game.Inventory;

namespace Game.Store
{
    public class EquipRewardResultVM : RewardResultVM
    {
        public EquipDataVM EquipVM { get; }

        public EquipRewardResultVM(EquipRewardResult result, StoreVMFactory storeVMF, InventoryVMFactory inventoryVMF) 
            : base(result, storeVMF)
        {
            var itemDataVM = inventoryVMF.CreateItemData(result.ItemDataId);

            EquipVM = itemDataVM as EquipDataVM;
        }

        protected override void InitSubscribes()
        {
            base.InitSubscribes();

            EquipVM.AddTo(this);
        }
    }
}