using AD.Services.Localization;
using AD.ToolsCollection;
using Game.Inventory;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Guild
{
    public class GuildBankTabData : ScriptableEntity<string>
    {
        [SerializeField] private LocalizeKey nameKey;
        [SerializeField] private AssetReference iconRef;
        [SerializeField] private ItemsGridBaseData grid;

        public LocalizeKey NameKey => nameKey;
        public AssetReference IconRef => iconRef;
        public ItemsGridBaseData Grid => grid;
    }
}