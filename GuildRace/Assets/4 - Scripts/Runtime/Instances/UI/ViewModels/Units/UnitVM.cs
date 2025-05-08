using AD.Services.Localization;
using AD.Services.Router;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using Game.Guild;
using UnityEngine;

namespace Game.Instances
{
    public class UnitVM : ViewModel
    {
        private readonly AddressableSprite imageRef;

        public int Id { get; }

        public LocalizeKey NameKey { get; }
        public LocalizeKey DescKey { get; }
        public AbilitiesVM AbilitiesVM { get; }

        public UnitVM(UnitInfo info, InstancesConfig config)
        {
            Id = info.Id;
            NameKey = info.NameKey;
            DescKey = info.DescKey;
            imageRef = info.ImageRef;
            AbilitiesVM = new AbilitiesVM(info.Abilities, config);
        }

        protected override void InitSubscribes()
        {
            imageRef.AddTo(this);
            AbilitiesVM.AddTo(this);
        }

        public async UniTask<Sprite> LoadImage()
        {
            return await imageRef.LoadAsync();
        }
    }
}