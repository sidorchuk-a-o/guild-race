using R3;

namespace AD.Game.Instances
{
    public class UnitCooldownInfo
    {
        private readonly ReactiveProperty<int> maxTriesCount = new();

        public bool IsWeeklyReset { get; }
        public InstanceType InstanceType { get; }

        public int MaxCompletedCount { get; }
        public ReadOnlyReactiveProperty<int> MaxTriesCount => maxTriesCount;

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