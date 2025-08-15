using AD.Services.Router;
using Game.Craft;

namespace Game.Store
{
    public class ReagentRewardVM : ViewModel
    {
        public ReagentDataVM ReagentVM { get; }
        public int Amount { get; }

        public ReagentRewardVM(ReagentRewardData data, CraftVMFactory craftVMF)
        {
            Amount = data.Amount;
            ReagentVM = craftVMF.InventoryVMF.CreateItemData(data.ReagentId) as ReagentDataVM;
        }

        protected override void InitSubscribes()
        {
            ReagentVM.AddTo(this);
        }
    }
}