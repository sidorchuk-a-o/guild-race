using AD.Services.ProtectedTime;
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
        private readonly ReactiveProperty<string> completeChanceStr = new();

        public string Id { get; }

        public InstanceVM InstanceVM { get; }
        public UnitVM BossUnitVM { get; }

        public ThreatsVM ThreatsVM { get; }
        public SquadUnitsVM SquadVM { get; }

        public TimerVM TimerVM { get; }
        public UIStateVM ResultStateVM { get; }
        public IReadOnlyReactiveProperty<float> CompleteChance { get; }
        public IReadOnlyReactiveProperty<string> CompleteChanceStr => completeChanceStr;

        public IReadOnlyReactiveProperty<bool> IsReadyToComplete { get; }

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
            RewardsVM = GetRewards(info, instancesVMF);
            AdsRewardVM = GetAdsRewards(info, instancesVMF);
            ResultStateVM = new();

            var startTime = info.StartTime;
            var timerTime = info.BossUnit.CompleteTime;

            TimerVM = new TimerVM(startTime, timerTime, instancesVMF.TimeService);
        }

        private InstanceRewardsVM GetRewards(ActiveInstanceInfo info, InstancesVMFactory instancesVMF)
        {
            return instancesVMF.GetRewards(info.Rewards);
        }

        private AdsInstanceRewardsVM GetAdsRewards(ActiveInstanceInfo info, InstancesVMFactory instancesVMF)
        {
            return instancesVMF.GetAdsRewards(info.AdsRewards);
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

            completeChanceStr.Value = $"{Mathf.RoundToInt(chance * 100)}%";

#if UNITY_EDITOR
            completeChanceStr.Value += " <i><size=12>(debug)</size></i>";
#endif
        }
    }
}