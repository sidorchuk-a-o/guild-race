using System.Threading;
using AD.Services.Localization;
using AD.Services.Router;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Instances
{
    public class InstanceVM : ViewModel
    {
        private readonly InstanceInfo info;
        private readonly InstancesVMFactory instancesVMF;

        public int Id { get; }

        public InstanceType Type { get; }
        public LocalizeKey NameKey { get; }
        public LocalizeKey DescKey { get; }

        public UnitsVM BossUnitsVM { get; }

        public InstanceVM(InstanceInfo info, InstancesVMFactory instancesVMF)
        {
            this.info = info;
            this.instancesVMF = instancesVMF;

            Id = info.Id;
            Type = info.Type;
            NameKey = info.NameKey;
            DescKey = info.DescKey;
            BossUnitsVM = new UnitsVM(info.BossUnits, instancesVMF);
        }

        protected override void InitSubscribes()
        {
            BossUnitsVM.AddTo(this);
        }

        public async UniTask<Sprite> LoadPreviewImage(CancellationTokenSource ct)
        {
            return await instancesVMF.LoadImage(info.ImageRef, ct);
        }

        public async UniTask<Sprite> LoadLoadingImage(CancellationTokenSource ct)
        {
            return await instancesVMF.LoadImage(info.LoadingRef, ct);
        }
    }
}