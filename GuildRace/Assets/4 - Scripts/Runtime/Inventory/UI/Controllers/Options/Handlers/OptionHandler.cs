using AD.Services.Localization;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Inventory
{
    public abstract class OptionHandler : ScriptableEntity
    {
        [SerializeField] private LocalizeKey titleKey;
        [SerializeField] private AssetReference iconRef;
        [SerializeField] private AssetReference buttonRef;

        public OptionKey Key => new(Id);
        public LocalizeKey TitleKey => titleKey;

        public AssetReference IconRef => iconRef;
        public AssetReference ButtonRef => buttonRef;

        public abstract UniTask StartProcess(OptionContext context);
    }
}