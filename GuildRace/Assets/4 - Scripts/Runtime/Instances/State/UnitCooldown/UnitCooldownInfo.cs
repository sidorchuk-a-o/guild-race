using UniRx;

namespace Game.Instances
{
    public class UnitCooldownInfo
    {
        private readonly ReactiveProperty<int> maxTriesCount = new();

        public InstanceType InstanceType { get; }
        public int MaxCompletedCount { get; }
        public IReadOnlyReactiveProperty<int> MaxTriesCount => maxTriesCount;
        public bool IsWeeklyReset { get; }

        public UnitCooldownInfo(UnitCooldownParams data)
        {
            InstanceType = data.InstanceType;
            MaxCompletedCount = data.MaxCompletedCount;
            IsWeeklyReset = data.IsWeeklyReset;

            maxTriesCount.Value = data.MaxTriesCount;
        }

        public void SetMaxTriesCount(int count)
        {
            maxTriesCount.Value = count;
        }
    }
}