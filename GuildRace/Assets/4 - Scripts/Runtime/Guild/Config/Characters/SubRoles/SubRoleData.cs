using AD.Services.Localization;
using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Guild
{
    public class SubRoleData : ScriptableEntity<int>
    {
        [SerializeField] private LocalizeKey nameKey;
        [SerializeField] private AssetReference iconRef;

        public LocalizeKey NameKey => nameKey;
        public AssetReference IconRef => iconRef;
    }
}