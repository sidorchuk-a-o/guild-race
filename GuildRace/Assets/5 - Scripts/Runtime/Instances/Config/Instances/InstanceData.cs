using System;
using System.Collections.Generic;
using AD.Localization;
using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AD.Game.Instances
{
    [Serializable]
    public class InstanceData : Entity<int>
    {
        [SerializeField] private LocalizeKey nameKey;
        [SerializeField] private LocalizeKey descKey;
        [SerializeField] private AssetReference imageRef;
        [SerializeField] private AssetReference loadingRef;
        [Space]
        [SerializeField] private InstanceType type;
        [SerializeField] private List<UnitData> boosUnits;

        public LocalizeKey NameKey => nameKey;
        public LocalizeKey DescKey => descKey;
        public AssetReference ImageRef => imageRef;
        public AssetReference LoadingRef => loadingRef;

        public InstanceType Type => type;
        public IReadOnlyList<UnitData> BoosUnits => boosUnits;
    }
}