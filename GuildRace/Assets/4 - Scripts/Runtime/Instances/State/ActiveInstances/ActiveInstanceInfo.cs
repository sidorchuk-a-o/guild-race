using System;
using System.Linq;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using AD.ToolsCollection;
using UniRx;
using UnityEngine;

namespace Game.Instances
{
    public class ActiveInstanceInfo : IEquatable<ActiveInstanceInfo>
    {
        private readonly ThreatCollection threats;
        private readonly SquadUnitsCollection squad;
        private readonly ReactiveProperty<CompleteResult> completeResult = new();
        private readonly ReactiveProperty<float> completeChance = new();
        private readonly ReactiveProperty<bool> isReadyToComplete = new();
        private readonly ReactiveProperty<long> startTime = new();
        private readonly List<RewardResult> rewards = new();
        private readonly List<AdsInstanceRewardInfo> adsRewards = new();

        public string Id { get; }

        public UnitInfo BossUnit { get; }
        public InstanceInfo Instance { get; }

        public IThreatCollection Threats => threats;
        public ISquadUnitsCollection Squad => squad;

        public IReadOnlyReactiveProperty<long> StartTime => startTime;
        public IReadOnlyList<RewardResult> Rewards => rewards;
        public IReadOnlyList<AdsInstanceRewardInfo> AdsRewards => adsRewards;
        
        public IReadOnlyReactiveProperty<float> CompleteChance => completeChance;
        public IReadOnlyReactiveProperty<CompleteResult> Result => completeResult;
        public IReadOnlyReactiveProperty<bool> IsReadyToComplete => isReadyToComplete;

        public ActiveInstanceInfo(string id, InstanceInfo inst, UnitInfo bossUnit, IEnumerable<SquadUnitInfo> squad)
        {
            Id = id;
            Instance = inst;
            BossUnit = bossUnit;

            this.squad = new(squad);

            threats = new(bossUnit.Threats.Select(x => new ThreatInfo(x)));
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
            var threatGroups = resolvedThreats.GroupBy(x => x).ToListPool();

            foreach (var threat in threats)
            {
                var threatGroup = threatGroups.FirstOrDefault(x => x.Key == threat.Id);
                var resolveCount = threatGroup == null ? 0 : threatGroup.Count();

                threat.SetResolveCount(resolveCount);
            }

            threatGroups.ReleaseListPool();
        }

        public void SetStartTime(long value)
        {
            startTime.Value = value;
        }

        public void SetCompleteChance(float value)
        {
            this.LogMsg($"Complete Chance: {Mathf.RoundToInt(value * 100f)}%");

            completeChance.Value = value;
        }

        public void SetResult(CompleteResult value)
        {
            completeResult.Value = value;
        }

        public void MarkAsReadyToComplete()
        {
            isReadyToComplete.Value = true;
        }

        public void SetRewards(IEnumerable<RewardResult> rewards)
        {
            this.rewards.AddRange(rewards);
        }

        public void SetAdsRewards(IEnumerable<AdsInstanceRewardInfo> adsRewards)
        {
            this.adsRewards.AddRange(adsRewards);
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