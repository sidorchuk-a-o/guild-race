using System;
using System.Collections.Generic;
using System.Linq;
using AD.Game.Guild;
using AD.Localization;
using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AD.Game.Instances
{
    [Serializable]
    public class UnitData : Entity<int>
    {
        [SerializeField] private LocalizeKey nameKey;
        [SerializeField] private LocalizeKey descKey;
        [SerializeField] private AssetReference imageRef;
        [Space]
        [SerializeField] private int completeTime;
        [SerializeField] private UnitParams unitParams;
        [SerializeField] private List<AbilityData> abilities;

        public LocalizeKey NameKey => nameKey;
        public LocalizeKey DescKey => descKey;
        public AssetReference ImageRef => imageRef;

        public int CompleteTime => completeTime;
        public UnitParams UnitParams => unitParams;

        public IReadOnlyList<AbilityData> Abilities => abilities;
        public IEnumerable<ThreatId> Threats => abilities.Select(x => x.ThreatId).Distinct();
    }
}