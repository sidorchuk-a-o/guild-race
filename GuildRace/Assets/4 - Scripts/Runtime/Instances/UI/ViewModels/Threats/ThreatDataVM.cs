using System.Threading;
using AD.Services.Localization;
using AD.Services.Router;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Instances
{
    public class ThreatDataVM : ViewModel
    {
        private readonly ThreatData data;
        private readonly InstancesVMFactory instancesVMF;

        public ThreatId Id { get; }
        public LocalizeKey NameKey { get; }
        public LocalizeKey DescKey { get; }

        public ThreatDataVM(ThreatData data, InstancesVMFactory instancesVMF)
        {
            this.data = data;
            this.instancesVMF = instancesVMF;

            Id = data.Id;
            NameKey = data.NameKey;
            DescKey = data.DescKey;
        }

        protected override void InitSubscribes()
        {
        }

        public async UniTask<Sprite> LoadIcon(CancellationTokenSource ct)
        {
            return await instancesVMF.LoadImage(data.IconRef, ct);
        }
    }
}