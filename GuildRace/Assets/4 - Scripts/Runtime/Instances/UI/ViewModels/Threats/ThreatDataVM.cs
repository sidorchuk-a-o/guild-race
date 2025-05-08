using System.Threading;
using AD.Services.Localization;
using AD.Services.Router;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Game.Instances
{
    public class ThreatDataVM : ViewModel
    {
        private readonly AddressableSprite iconRef;

        public ThreatId Id { get; }
        public LocalizeKey NameKey { get; }
        public LocalizeKey DescKey { get; }

        public ThreatDataVM(ThreatData data)
        {
            Id = data.Id;
            NameKey = data.NameKey;
            DescKey = data.DescKey;
            iconRef = data.IconRef;
        }

        protected override void InitSubscribes()
        {
            iconRef.AddTo(this);
        }

        public async UniTask<Sprite> LoadIcon(CancellationTokenSource ct)
        {
            return await iconRef.LoadAsync(ct.Token);
        }
    }
}