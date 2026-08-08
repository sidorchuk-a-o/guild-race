using AD.Router;
using R3;

namespace AD.Game.Instances
{
    public class AdsInstanceRewardVM : ViewModel
    {
        public InstanceRewardVM RewardVM { get; }
        public ReadOnlyReactiveProperty<bool> IsRewarded { get; }

        public AdsInstanceRewardVM(AdsInstanceRewardInfo info, InstancesVMFactory instancesVMF)
        {
            RewardVM = instancesVMF.GetReward(info.Id);
            IsRewarded = info.IsRewarded;
        }

        protected override void InitSubscribes()
        {
            RewardVM.AddTo(this);
        }
    }
}