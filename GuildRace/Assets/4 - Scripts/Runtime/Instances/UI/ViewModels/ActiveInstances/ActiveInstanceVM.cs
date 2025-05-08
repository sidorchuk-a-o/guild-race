using AD.Services.Router;
using UniRx;
using UnityEngine;

namespace Game.Instances
{
    public class ActiveInstanceVM : ViewModel
    {
        private readonly ActiveInstanceInfo info;
        private readonly ReactiveProperty<string> completeChance = new();

        public string Id { get; }

        public InstanceVM InstanceVM { get; }

        public UnitVM BossUnitVM { get; }
        public SquadUnitsVM SquadVM { get; }

        public IReadOnlyReactiveProperty<string> CompleteChance => completeChance;
        public IReadOnlyReactiveProperty<bool> IsReadyToComplete { get; }

        public ActiveInstanceVM(ActiveInstanceInfo info, InstancesVMFactory instancesVMF)
        {
            this.info = info;

            Id = info.Id;
            IsReadyToComplete = info.IsReadyToComplete;

            InstanceVM = instancesVMF.GetInstance(info.Instance);
            BossUnitVM = instancesVMF.GetUnit(info.BossUnit);
            SquadVM = new SquadUnitsVM(info.Instance.Type, info.Squad, instancesVMF);
        }

        protected override void InitSubscribes()
        {
            InstanceVM.AddTo(this);
            BossUnitVM.AddTo(this);
            SquadVM.AddTo(this);

            info.CompleteChance
                .Subscribe(x => completeChance.Value = $"{Mathf.RoundToInt(x * 100)}%")
                .AddTo(this);
        }
    }
}