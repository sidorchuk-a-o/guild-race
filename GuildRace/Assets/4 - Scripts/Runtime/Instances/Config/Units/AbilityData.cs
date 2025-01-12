using AD.Services.Localization;
using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Instances
{
    public class AbilityData : ScriptableEntity<int>
    {
        // params
        [SerializeField] private LocalizeKey nameKey;
        [SerializeField] private LocalizeKey descKey;
        // ui
        [SerializeField] private AssetReference iconRef;
    }
}