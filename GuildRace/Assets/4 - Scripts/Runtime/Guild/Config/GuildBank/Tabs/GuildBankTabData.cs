using AD.Services.Localization;
using AD.ToolsCollection;
using Game.Inventory;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Guild
{
    public class GuildBankTabData : ScriptableEntity
    {
        [SerializeField] private LocalizeKey nameKey;
        [SerializeField] private AssetReference iconRef;
        [SerializeField] private ItemsGridData grid;

        public LocalizeKey NameKey => nameKey;
        public AssetReference IconRef => iconRef;
        public ItemsGridData Grid => grid;
    }
}