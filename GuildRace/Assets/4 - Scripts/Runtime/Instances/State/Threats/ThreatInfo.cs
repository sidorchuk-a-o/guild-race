using UniRx;

namespace Game.Instances
{
    public class ThreatInfo
    {
        private readonly ReactiveProperty<bool> resolved = new();

        public ThreatId ThreatId { get; }
        public IReadOnlyReactiveProperty<bool> Resolved => resolved;

        public ThreatInfo(ThreatId threatId)
        {
            ThreatId = threatId;
        }

        public ThreatInfo(ThreatId threatId, bool resolved)
        {
            ThreatId = threatId;

            SetResolvedState(resolved);
        }

        private void SetResolvedState(bool value)
        {
            resolved.Value = value;
        }
    }
}