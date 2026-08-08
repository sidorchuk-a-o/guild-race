using AD.Game.Craft;
using AD.Game.Inventory;
using AD.Router;
using AD.Store;

namespace AD.Game.Store
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