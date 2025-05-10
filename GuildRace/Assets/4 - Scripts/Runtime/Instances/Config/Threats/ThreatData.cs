using System;
using AD.Services.Localization;
using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Instances
{
    [Serializable]
    public class ThreatData : Entity<int>
    {
        [SerializeField] private LocalizeKey nameKey;
        [SerializeField] private LocalizeKey descKey;
        [SerializeField] private AssetReference iconRef;

        public LocalizeKey NameKey => nameKey;
        public LocalizeKey DescKey => descKey;
        public AssetReference IconRef => iconRef;
    }
}