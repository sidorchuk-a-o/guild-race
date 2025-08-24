using AD.Services.Router;
using AD.Services.Store;
using AD.UI;
using UniRx;

namespace Game.Quests
{
    public class QuestVM : ViewModel
    {
        private readonly QuestInfo info;
        private readonly QuestsVMFactory questsVMF;
        private readonly QuestMechanicVM mechanicVM;

        private readonly ReactiveProperty<string> progressStr = new();
        private readonly ReactiveProperty<CurrencyAmount> reward = new();

        public string Id { get; }
        public QuestsGroup GroupId { get; }

        public UITextData NameKey { get; }
        public UITextData DescKey { get; }
        public CurrencyVM RewardVM { get; }

        public int RequiredProgress { get; }
        public IReadOnlyReactiveProperty<int> ProgressCounter { get; }
        public IReadOnlyReactiveProperty<string> ProgressStr => progressStr;

        public IReadOnlyReactiveProperty<bool> IsCompleted { get; }
        public IReadOnlyReactiveProperty<bool> IsRewarded { get; }

        public QuestVM(QuestInfo info, QuestsVMFactory questsVMF)
        {
            this.info = info;
            this.questsVMF = questsVMF;

            reward.Value = info.Reward;
            RewardVM = questsVMF.StoreVMF.GetCurrency(reward);

            mechanicVM = questsVMF.GetMechanic(info.MechanicId);

            Id = info.Id;
            GroupId = info.GroupId;
            IsRewarded = info.IsRewarded;
            IsCompleted = info.IsCompleted;
            RequiredProgress = info.RequiredProgress;
            ProgressCounter = info.ProgressCounter;
            NameKey = mechanicVM.GetNameKey(info);
            DescKey = mechanicVM.GetDescKey(info);
        }

        protected override void InitSubscribes()
        {
            mechanicVM.AddTo(this);
            RewardVM.AddTo(this);

            info.ProgressCounter
                .Subscribe(ProgressChangedCallback)
                .AddTo(this);

            questsVMF.RewardBonus
                .Subscribe(RewardBonusChangedCallback)
                .AddTo(this);
        }

        private void ProgressChangedCallback(int progress)
        {
            progressStr.Value = $"{progress} / {info.RequiredProgress}";
        }

        private void RewardBonusChangedCallback(float bonusValue)
        {
            var bonus = info.Reward * bonusValue;

            reward.Value = info.Reward + bonus;
        }
    }
}