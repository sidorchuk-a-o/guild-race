using AD.Services.Localization;
using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Instances
{
    public class InstanceData : ScriptableEntity<int>
    {
        // params
        [SerializeField] private InstanceType type;
        [SerializeField] private LocalizeKey nameKey;
        [SerializeField] private LocalizeKey descKey;
        // map logic
        [SerializeField] private AssetReference mapRef;

        public LocalizeKey NameKey => nameKey;
        public LocalizeKey DescKey => descKey;
        public InstanceType Type => type;
        public AssetReference MapRef => mapRef;
    }
}