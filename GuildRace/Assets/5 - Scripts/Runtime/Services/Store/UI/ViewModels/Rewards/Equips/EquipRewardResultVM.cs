using AD.Game.Inventory;
using AD.Router;
using AD.Store;

namespace AD.Game.Store
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