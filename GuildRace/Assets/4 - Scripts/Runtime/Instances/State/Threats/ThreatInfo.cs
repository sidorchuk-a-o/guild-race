using UniRx;

namespace Game.Instances
{
    public class ThreatInfo
    {
        private readonly ReactiveProperty<bool> resolved = new();

        public ThreatId Id { get; }

        public int ResolveCount { get; private set; }
        public IReadOnlyReactiveProperty<bool> Resolved => resolved;

        public ThreatInfo(ThreatId id)
        {
            Id = id;
        }

        public ThreatInfo(ThreatId id, int resolveCount)
        {
            Id = id;

            SetResolveCount(resolveCount);
        }

        public void SetResolveCount(int value)
        {
            ResolveCount = value;
            resolved.Value = value > 0;
        }
    }
}