using System.Collections.Generic;
using System.Linq;
using AD.Services.Localization;
using AD.ToolsCollection;
using Game.Guild;
using UniRx;
using UnityEngine.AddressableAssets;

namespace Game.Instances
{
    public class UnitInfo
    {
        private readonly ReactiveProperty<string> instanceId = new();

        public int Id { get; }

        public LocalizeKey NameKey { get; }
        public LocalizeKey DescKey { get; }
        public AssetReference ImageRef { get; }

        public int CompleteTime { get; }
        public UnitParams UnitParams { get; }

        public IReadOnlyCollection<AbilityData> Abilities { get; }
        public IReadOnlyCollection<ThreatId> Threats { get; }

        public bool HasInstance => instanceId.Value.IsValid();
        public IReadOnlyReactiveProperty<string> InstanceId => instanceId;

        public UnitInfo(UnitData data)
        {
            Id = data.Id;
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
    }
}