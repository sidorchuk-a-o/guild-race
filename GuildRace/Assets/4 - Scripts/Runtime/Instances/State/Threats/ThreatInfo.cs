using UniRx;

namespace Game.Instances
{
    public class ThreatInfo
    {
        private readonly ReactiveProperty<bool> resolved = new();

        public ThreatId Id { get; }
        public IReadOnlyReactiveProperty<bool> Resolved => resolved;

        public ThreatInfo(ThreatId id)
        {
            Id = id;
        }

        public ThreatInfo(ThreatId id, bool resolved)
        {
            Id = id;

            SetResolvedState(resolved);
        }

        public void SetResolvedState(bool value)
        {
            resolved.Value = value;
        }
    }
}