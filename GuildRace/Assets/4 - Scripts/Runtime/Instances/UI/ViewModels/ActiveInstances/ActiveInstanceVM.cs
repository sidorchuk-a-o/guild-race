using AD.Services.Router;
using AD.ToolsCollection;
using Game.UI;
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

        public ThreatsVM ThreatsVM { get; }
        public SquadUnitsVM SquadVM { get; }

        public TimerVM TimerVM { get; }
        public UIStateVM ResultStateVM { get; }
        public IReadOnlyReactiveProperty<string> CompleteChance => completeChance;
        public IReadOnlyReactiveProperty<bool> IsReadyToComplete { get; }

        public InstanceRewardsVM RewardsVM { get; }

        public ActiveInstanceVM(ActiveInstanceInfo info, InstancesVMFactory instancesVMF)
        {
            this.info = info;

            Id = info.Id;
            IsReadyToComplete = info.IsReadyToComplete;

            InstanceVM = instancesVMF.GetInstance(info.Instance);
            BossUnitVM = new(info.BossUnit, info.Instance.Id, instancesVMF);
            ThreatsVM = new(info.Threats, instancesVMF);
            SquadVM = new(info.Instance.Type, info.Squad, instancesVMF);
            RewardsVM = GetRewards(info, instancesVMF);
            ResultStateVM = new();

            var startTime = info.StartTime;
            var timerTime = info.BossUnit.CompleteTime;

            TimerVM = new TimerVM(startTime, timerTime, instancesVMF.TimeService);
        }

        private InstanceRewardsVM GetRewards(ActiveInstanceInfo info, InstancesVMFactory instancesVMF)
        {
            return info.Rewards != null
                ? instancesVMF.GetRewards(info.Rewards)
                : null;
        }

        protected override void InitSubscribes()
        {
            InstanceVM.AddTo(this);
            BossUnitVM.AddTo(this);
            ThreatsVM.AddTo(this);
            SquadVM.AddTo(this);
            TimerVM.AddTo(this);
            RewardsVM?.AddTo(this);
            ResultStateVM.AddTo(this);

            info.Result
                .Subscribe(x => ResultStateVM.SetState(GetResultState(x)))
                .AddTo(this);

            info.CompleteChance
                .Subscribe(x => ChanceChangedCallback(x))
                .AddTo(this);
        }

        private string GetResultState(CompleteResult result)
        {
            return result switch
            {
                CompleteResult.Failed => "failed",
                _ => "default"
            };
        }

        private void ChanceChangedCallback(float chance)
        {
#if !UNITY_EDITOR
            chance = Mathf.Max(chance, 0);
#endif

            completeChance.Value = $"{Mathf.RoundToInt(chance * 100)}%";
        }
    }
}