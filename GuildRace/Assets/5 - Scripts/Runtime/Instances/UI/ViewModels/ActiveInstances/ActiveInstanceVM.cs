using AD.Game.UI;
using AD.ProtectedTime;
using AD.Router;
using R3;
using UnityEngine;

namespace AD.Game.Instances
{
    public class ActiveInstanceVM : ViewModel
    {
        private readonly ActiveInstanceInfo info;
        private readonly ReactiveProperty<string> completeChanceStr = new();

        public string Id { get; }

        public InstanceVM InstanceVM { get; }
        public UnitVM BossUnitVM { get; }

        public ThreatsVM ThreatsVM { get; }
        public SquadUnitsVM SquadVM { get; }

        public TimerVM TimerVM { get; }
        public UIStateVM ResultStateVM { get; }
        public ReadOnlyReactiveProperty<float> CompleteChance { get; }
        public ReadOnlyReactiveProperty<string> CompleteChanceStr => completeChanceStr;

        public ReadOnlyReactiveProperty<bool> IsReadyToComplete { get; }

        public InstanceRewardsVM RewardsVM { get; }
        public AdsInstanceRewardsVM AdsRewardVM { get; }

        public ActiveInstanceVM(ActiveInstanceInfo info, InstancesVMFactory instancesVMF)
        {
            this.info = info;

            Id = info.Id;
            IsReadyToComplete = info.IsReadyToComplete;

            InstanceVM = instancesVMF.GetInstance(info.Instance);
            BossUnitVM = new(info.BossUnit, instancesVMF);
            ThreatsVM = new(info.Threats, instancesVMF);
            SquadVM = new(info.Instance.Type, info.Squad, instancesVMF);
            CompleteChance = info.CompleteChance;
            RewardsVM = instancesVMF.GetRewards(info.Rewards);
            AdsRewardVM = instancesVMF.GetAdsRewards(info.AdsRewards);
            ResultStateVM = new();

            var startTime = info.StartTime;
            var timerTime = info.BossUnit.CompleteTime;

            TimerVM = new(startTime, timerTime, instancesVMF.TimeService);
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
                .Subscribe(ChanceChangedCallback)
                .AddTo(this);
        }

        private static string GetResultState(CompleteResult result)
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

            completeChanceStr.Value = $"{Mathf.RoundToInt(chance * 100)}%";

#if UNITY_EDITOR
            completeChanceStr.Value += " <i><size=12>(debug)</size></i>";
#endif
        }
    }
}