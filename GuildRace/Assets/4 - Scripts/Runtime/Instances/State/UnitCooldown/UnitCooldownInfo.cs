using UniRx;

namespace Game.Instances
{
    public class UnitCooldownInfo
    {
        private readonly ReactiveProperty<int> maxTriesCount = new();

        public bool IsWeeklyReset { get; }
        public InstanceType InstanceType { get; }

        public int MaxCompletedCount { get; }
        public IReadOnlyReactiveProperty<int> MaxTriesCount => maxTriesCount;

        public UnitCooldownInfo(UnitCooldownParams data)
        {
            InstanceType = data.InstanceType;
            IsWeeklyReset = data.IsWeeklyReset;
            MaxCompletedCount = data.MaxCompletedCount;

            maxTriesCount.Value = data.MaxTriesCount;
        }

        public void SetMaxTriesCount(int count)
        {
            maxTriesCount.Value = count;
        }
    }
}