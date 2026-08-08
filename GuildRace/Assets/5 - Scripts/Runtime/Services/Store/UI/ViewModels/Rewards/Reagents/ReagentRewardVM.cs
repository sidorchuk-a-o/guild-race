using AD.Game.Craft;
using AD.Router;

namespace AD.Game.Store
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