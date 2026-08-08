using R3;

namespace AD.Game.Instances
{
    public class AdsInstanceRewardInfo
    {
        private readonly ReactiveProperty<bool> isRewarded = new();

        public int Id { get; }
        public InstanceRewardData Data { get; }
        public ReadOnlyReactiveProperty<bool> IsRewarded => isRewarded;

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