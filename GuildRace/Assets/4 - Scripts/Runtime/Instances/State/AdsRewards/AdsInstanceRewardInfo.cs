using UniRx;

namespace Game.Instances
{
    public class AdsInstanceRewardInfo
    {
        private readonly ReactiveProperty<bool> isRewarded = new();

        public int Id { get; }
        public InstanceRewardData Data { get; }
        public IReadOnlyReactiveProperty<bool> IsRewarded => isRewarded;

        public AdsInstanceRewardInfo(InstanceRewardData data)
        {
            Id = data.Id;
            Data = data;
        }

        public void MarkAsRewarded()
        {
            isRewarded.Value = true;
        }
    }
}