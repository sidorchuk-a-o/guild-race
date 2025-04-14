using AD.Services.Localization;
using AD.ToolsCollection;
using Game.Guild;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Instances
{
    public class UnitData : ScriptableEntity<int>
    {
        [SerializeField] private LocalizeKey nameKey;
        [SerializeField] private LocalizeKey descKey;
        [SerializeField] private AssetReference imageRef;
        [Space]
        [SerializeField] private UnitParams unitParams;
        [SerializeField] private List<AbilityData> abilities;

        public LocalizeKey NameKey => nameKey;
        public LocalizeKey DescKey => descKey;
        public AssetReference ImageRef => imageRef;

        public UnitParams UnitParams => unitParams;
        public IReadOnlyList<AbilityData> Abilities => abilities;
    }
}