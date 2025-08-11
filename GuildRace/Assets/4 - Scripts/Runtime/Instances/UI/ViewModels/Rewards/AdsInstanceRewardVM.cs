using AD.Services.Router;
using UniRx;

namespace Game.Instances
{
    public class AdsInstanceRewardVM : ViewModel
    {
        public InstanceRewardVM RewardVM { get; }
        public IReadOnlyReactiveProperty<bool> IsRewarded { get; }

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