using AD.Services.Localization;
using AD.ToolsCollection;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Inventory
{
    public abstract class ItemData : ScriptableEntity<int>
    {
        // Params
        [SerializeField] private ItemSlot slot;
        [SerializeField] private ItemSize size = ItemSize.Default;
        // View
        [SerializeField] private LocalizeKey nameKey;
        [SerializeField] private AssetReference iconRef;

        public ItemSlot Slot => slot;
        public ItemSize Size => size;

        public LocalizeKey NameKey => nameKey;
        public AssetReference IconRef => iconRef;
    }
}