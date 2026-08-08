using System.Collections.Generic;
using AD.Game.Craft;
using AD.Router;
using AD.Store;

namespace AD.Game.Store
{
    public class ReagentsRewardVM : RewardVM
    {
        public ReagentRewardsVM ReagentRewardsVM { get; }

        public ReagentsRewardVM(ReagentsReward data, StoreVMFactory storeVMF, CraftVMFactory craftVMF) : base(data, storeVMF)
        {
            ReagentRewardsVM = new(data.ReagentRewards, craftVMF);
        }

        protected override void InitSubscribes()
        {
            ReagentRewardsVM.AddTo(this);

            base.InitSubscribes();
        }

        protected override IEnumerable<RewardPreviewVM> GetRewardPreviews()
        {
            for (var i = 0; i < ReagentRewardsVM.Count; i++)
            {
                var reagentVM = ReagentRewardsVM[i];

                yield return new(reagentVM.Amount, reagentVM.ReagentVM.IconRef, storeVMF);
            }
        }
    }
}