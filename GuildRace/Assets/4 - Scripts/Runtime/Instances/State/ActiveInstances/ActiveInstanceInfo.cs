using System;
using System.Collections.Generic;
using System.Linq;
using AD.ToolsCollection;
using UniRx;

namespace Game.Instances
{
    public class ActiveInstanceInfo : IEquatable<ActiveInstanceInfo>
    {
        private readonly ThreatCollection threats;
        private readonly SquadUnitsCollection squad;
        private readonly ReactiveProperty<float> completeChance = new();
        private readonly ReactiveProperty<bool> isReadyToComplete = new();

        public string Id { get; }

        public UnitInfo BossUnit { get; }
        public InstanceInfo Instance { get; }

        public IThreatCollection Threats => threats;
        public ISquadUnitsCollection Squad => squad;

        public long StartTime { get; private set; }
        public IReadOnlyReactiveProperty<float> CompleteChance => completeChance;
        public IReadOnlyReactiveProperty<bool> IsReadyToComplete => isReadyToComplete;

        public ActiveInstanceInfo(string id, InstanceInfo inst, UnitInfo bossUnit, IEnumerable<SquadUnitInfo> squad)
        {
            Id = id;
            Instance = inst;
            BossUnit = bossUnit;

            this.squad = new(squad);

            threats = new(bossUnit.GetThreats().Select(x => new ThreatInfo(x)));
        }

        public void AddUnit(SquadUnitInfo squadUnit)
        {
            squad.Add(squadUnit);
        }

        public void RemoveUnit(SquadUnitInfo squadUnit)
        {
            squad.Remove(squadUnit);
        }

        public void ApplyResolveThreats(IEnumerable<ThreatId> resolvedThreats)
        {
            var resolvedPool = resolvedThreats.ToListPool();

            foreach (var threat in threats)
            {
                var resolved = resolvedPool.Contains(threat.Id);

                threat.SetResolvedState(resolved);
            }

            resolvedPool.ReleaseListPool();
        }

        public void SetStartTime(long value)
        {
            StartTime = value;
        }

        public void SetCompleteChance(float value)
        {
            completeChance.Value = value;
        }

        public void MarAsReadyToComplete()
        {
            isReadyToComplete.Value = true;
        }

        // == IEquatable ==

        public bool Equals(ActiveInstanceInfo other)
        {
            return other != null
                && other.Id == Id;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ActiveInstanceInfo);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}