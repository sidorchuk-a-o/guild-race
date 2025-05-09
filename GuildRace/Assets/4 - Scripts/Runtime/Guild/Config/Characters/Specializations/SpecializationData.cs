using System.Collections.Generic;
using System.Linq;
using AD.Services.Localization;
using AD.ToolsCollection;
using Game.Instances;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Guild
{
    public class SpecializationData : ScriptableEntity<int>
    {
        [SerializeField] private LocalizeKey nameKey;
        [SerializeField] private LocalizeKey descKey;
        [SerializeField] private AssetReference iconRef;
        [Space]
        [SerializeField] private RoleId roleId;
        [SerializeField] private SubRoleId subRoleId;
        [Space]
        [SerializeField] private UnitParams unitParams;
        [SerializeField] private List<AbilityData> abilities;

        public LocalizeKey NameKey => nameKey;
        public LocalizeKey DescKey => descKey;
        public AssetReference IconRef => iconRef;

        public RoleId RoleId => roleId;
        public SubRoleId SubRoleId => subRoleId;

        public UnitParams UnitParams => unitParams;
        public IReadOnlyCollection<AbilityData> Abilities => abilities;
        public IEnumerable<ThreatId> Threats => abilities.Select(x => x.ThreatId).Distinct();
    }
}