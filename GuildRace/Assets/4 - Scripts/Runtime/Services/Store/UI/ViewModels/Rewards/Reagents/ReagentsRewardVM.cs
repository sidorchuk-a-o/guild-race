using System.Collections.Generic;
using AD.Services.Router;
using AD.Services.Store;
using Game.Craft;

namespace Game.Store
{
    public class ReagentsRewardVM : RewardVM
    {
        public ReagentRewardsVM ReagentRewardsVM { get; }

        public ReagentsRewardVM(ReagentsReward data, StoreVMFactory storeVMF, CraftVMFactory craftVMF) : base(data, storeVMF)
        {
            ReagentRewardsVM = new ReagentRewardsVM(data.ReagentRewards, craftVMF);
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

            yield break;
        }
    }
}