using AD.Services.Router;
using AD.Services.Store;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Craft
{
    public class CurrencyVM : ViewModel
    {
        private readonly AddressableSprite iconRef;

        public CurrencyVM(CurrencyData data)
        {
            iconRef = data.IconRef;
        }

        protected override void InitSubscribes()
        {
            iconRef.AddTo(this);
        }

        public async UniTask<Sprite> LoadIcon()
        {
            return await iconRef.LoadAsync();
        }
    }
}