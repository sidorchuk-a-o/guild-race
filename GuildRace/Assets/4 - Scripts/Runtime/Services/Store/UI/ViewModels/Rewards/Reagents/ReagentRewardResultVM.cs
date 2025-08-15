using AD.Services.Router;
using AD.Services.Store;
using Game.Craft;
using Game.Inventory;

namespace Game.Store
{
    public class ReagentRewardResultVM : RewardResultVM
    {
        public ReagentDataVM ReagentVM { get; }
        public int Count { get; }

        public ReagentRewardResultVM(ReagentRewardResult result, StoreVMFactory storeVMF, InventoryVMFactory inventoryVMF) : base(result, storeVMF)
        {
            var reagentDataVM = inventoryVMF.CreateItemData(result.ItemDataId);

            Count = result.Count;
            ReagentVM = reagentDataVM as ReagentDataVM;
        }

        protected override void InitSubscribes()
        {
            base.InitSubscribes();

            ReagentVM.AddTo(this);
        }
    }
}