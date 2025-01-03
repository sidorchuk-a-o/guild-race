using AD.Services.Localization;
using AD.ToolsCollection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Instances
{
    public class UnitData : ScriptableEntity<int>
    {
        // params
        [SerializeField] private LocalizeKey nameKey;
        [SerializeField] private LocalizeKey descKey;
        // ui
        [SerializeField] private AssetReference imageRef;
        // abilities
        [SerializeField] private List<AbilityData> abilities;

        public LocalizeKey NameKey => nameKey;
        public LocalizeKey DescKey => descKey;
        public AssetReference ImageRef => imageRef;
        public IReadOnlyList<AbilityData> Abilities => abilities;
    }
}