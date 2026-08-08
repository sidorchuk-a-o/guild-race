using System.Collections.Generic;
using System.Linq;
using AD.Game.Guild;
using AD.Localization;
using AD.ToolsCollection;
using R3;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AD.Game.Instances
{
    public class UnitInfo
    {
        private readonly ReactiveProperty<string> instanceId = new();
        private readonly ReactiveProperty<int> completedCount = new();
        private readonly ReactiveProperty<int> triesCount = new();
        private readonly ReactiveProperty<int> totalCompletedCount = new();

        public int Id { get; }
        public string Title { get; }

        public LocalizeKey NameKey { get; }
        public LocalizeKey DescKey { get; }
        public AssetReference ImageRef { get; }

        public int CompleteTime { get; }
        public UnitParams UnitParams { get; }

        public ReadOnlyReactiveProperty<int> TotalCompletedCount => totalCompletedCount;
        public ReadOnlyReactiveProperty<int> CompletedCount => completedCount;
        public ReadOnlyReactiveProperty<int> TriesCount => triesCount;

        public IReadOnlyCollection<AbilityData> Abilities { get; }
        public IReadOnlyCollection<ThreatId> Threats { get; }

        public bool HasInstance => instanceId.Value.IsValid();
        public ReadOnlyReactiveProperty<string> InstanceId => instanceId;

        public UnitInfo(UnitData data)
        {
            Id = data.Id;
            Title = data.Title;
            NameKey = data.NameKey;
            DescKey = data.DescKey;
            ImageRef = data.ImageRef;
            CompleteTime = data.CompleteTime;
            UnitParams = data.UnitParams;
            Abilities = data.Abilities;
            Threats = data.Threats.ToArray();
        }

        public void SetInstanceId(string value)
        {
            instanceId.Value = value;
        }

        public void SetCompletedCount(int totalCount, int count)
        {
            totalCompletedCount.Value = totalCount;
            completedCount.Value = count;
        }

        public void IncreaseCompletedCount()
        {
            totalCompletedCount.Value++;
            completedCount.Value++;
        }

        public void SetTriesCount(int value)
        {
            triesCount.Value = Mathf.Max(value, 0);
        }

        public void IncreaseTriesCount()
        {
            triesCount.Value++;
        }

        public void ResetCompletedState()
        {
            triesCount.Value = 0;
            completedCount.Value = 0;
        }
    }
}