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
        [SerializeField] private AssetReference uiRef;

        public InstanceType Type => type;
        public LocalizeKey NameKey => nameKey;
        public LocalizeKey DescKey => descKey;

        public AssetReference MapRef => mapRef;
        public AssetReference UIRef => uiRef;
    }
}