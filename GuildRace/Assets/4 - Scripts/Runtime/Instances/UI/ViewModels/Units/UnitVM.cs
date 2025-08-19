using AD.Services.Localization;
using AD.Services.Router;
using AD.ToolsCollection;
using System.Threading;
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

        private readonly ReactiveProperty<ActiveInstanceVM> activeInstanceVM = new();

        public int Id { get; }

        public LocalizeKey NameKey { get; }
        public LocalizeKey DescKey { get; }
        public AbilitiesVM AbilitiesVM { get; }
        public InstanceRewardsVM RewardsVM { get; }

        public bool HasActiveInstance => ActiveInstanceVM.Value != null;
        public IReadOnlyReactiveProperty<ActiveInstanceVM> ActiveInstanceVM => activeInstanceVM;

        public bool HasComplete { get; private set; }
        public bool HasTries { get; private set; }
        public IReadOnlyReactiveProperty<int> TotalCompletedCount { get; }
        public IReadOnlyReactiveProperty<int> CompletedCount { get; }
        public IReadOnlyReactiveProperty<int> TriesCount { get; }
        public UnitCooldownInfo CooldownParams { get; }

        public UnitVM(UnitInfo info, InstancesVMFactory instancesVMF)
        {
            this.info = info;
            this.instancesVMF = instancesVMF;

            Id = info.Id;
            NameKey = info.NameKey;
            DescKey = info.DescKey;
            AbilitiesVM = new(info.Abilities, instancesVMF);
            RewardsVM = instancesVMF.GetRewards(Id);

            TriesCount = info.TriesCount;
            CompletedCount = info.CompletedCount;
            TotalCompletedCount = info.TotalCompletedCount;
            CooldownParams = instancesVMF.GetUnitCooldown(Id);
        }

        protected override void InitSubscribes()
        {
            AbilitiesVM.AddTo(this);
            RewardsVM.AddTo(this);

            info.InstanceId
                .Subscribe(InstanceChangedCallback)
                .AddTo(this);

            info.CompletedCount
                .SilentSubscribe(UpdateHasStates)
                .AddTo(this);

            info.TriesCount
                .SilentSubscribe(UpdateHasStates)
                .AddTo(this);

            UpdateHasStates();
        }

        public async UniTask<Sprite> LoadImage(CancellationTokenSource ct)
        {
            return await instancesVMF.LoadImage(info.ImageRef, ct);
        }

        private void InstanceChangedCallback(string activeInstanceId)
        {
            activeInstanceVM.Value = activeInstanceId.IsValid()
                ? instancesVMF.GetActiveInstance(activeInstanceId)
                : null;
        }

        private void UpdateHasStates()
        {
            HasTries = instancesVMF.HasBossTries(Id);
            HasComplete = instancesVMF.HasBossComplete(Id);
        }
    }
}