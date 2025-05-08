using System;
using System.Collections.Generic;
using System.Linq;
using AD.Services.Localization;
using Game.Guild;
using UnityEngine.AddressableAssets;

namespace Game.Instances
{
    public class UnitInfo
    {
        public int Id { get; }

        public LocalizeKey NameKey { get; }
        public LocalizeKey DescKey { get; }
        public AssetReference ImageRef { get; }

        public int CompleteTime { get; }
        public UnitParams UnitParams { get; }
        public IReadOnlyCollection<AbilityData> Abilities { get; }

        public UnitInfo(UnitData data)
        {
            Id = data.Id;
            NameKey = data.NameKey;
            DescKey = data.DescKey;
            ImageRef = data.ImageRef;
            CompleteTime = data.CompleteTime;
            UnitParams = data.UnitParams;
            Abilities = data.Abilities.ToList();
        }

        public ThreatId[] GetThreats()
        {
            return Abilities
                .Select(x => x.ThreatId)
                .Distinct()
                .ToArray();
        }
    }
}