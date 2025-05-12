using AD.Services.Localization;
using AD.Services.Router;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using Game.Guild;
using UniRx;
using UnityEngine;

namespace Game.Instances
{
    public class UnitVM : ViewModel
    {
        private readonly UnitInfo info;
        private readonly InstancesVMFactory instancesVMF;

        private readonly AddressableSprite imageRef;
        private readonly ReactiveProperty<ActiveInstanceVM> instanceVM = new();

        public int Id { get; }

        public LocalizeKey NameKey { get; }
        public LocalizeKey DescKey { get; }
        public AbilitiesVM AbilitiesVM { get; }

        public bool HasInstance => InstanceVM.Value != null;
        public IReadOnlyReactiveProperty<ActiveInstanceVM> InstanceVM => instanceVM;

        public bool WaitResetCooldown { get; }
        public int CompletedCount { get; }
        public int MaxCompletedCount { get; }

        public UnitVM(UnitInfo info, int instanceId, InstancesVMFactory instancesVMF)
        {
            this.info = info;
            this.instancesVMF = instancesVMF;

            Id = info.Id;
            NameKey = info.NameKey;
            DescKey = info.DescKey;
            imageRef = info.ImageRef;
            AbilitiesVM = new AbilitiesVM(info.Abilities, instancesVMF);

            CompletedCount = info.CompletedCount;
            MaxCompletedCount = instancesVMF.GetMaxCompletedCount(instanceId);
            WaitResetCooldown = instancesVMF.CheckUnitCooldown(info.Id, instanceId);
        }

        protected override void InitSubscribes()
        {
            imageRef.AddTo(this);
            AbilitiesVM.AddTo(this);

            info.InstanceId
                .Subscribe(InstanceChangedCallback)
                .AddTo(this);
        }

        public async UniTask<Sprite> LoadImage()
        {
            return await imageRef.LoadAsync();
        }

        private void InstanceChangedCallback(string activeInstanceId)
        {
            instanceVM.Value = activeInstanceId.IsValid()
                ? instancesVMF.GetActiveInstance(activeInstanceId)
                : null;
        }
    }
}