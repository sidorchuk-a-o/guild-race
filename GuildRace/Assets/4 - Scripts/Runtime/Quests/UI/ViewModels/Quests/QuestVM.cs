using AD.Services.Router;
using AD.Services.Store;
using AD.UI;
using UniRx;

namespace Game.Quests
{
    public class QuestVM : ViewModel
    {
        private readonly QuestInfo info;
        private readonly QuestMechanicVM mechanicVM;

        private readonly ReactiveProperty<string> progressStr = new();

        public string Id { get; }
        public QuestsGroup GroupId { get; }

        public UITextData NameKey { get; }
        public UITextData DescKey { get; }
        public CurrencyAmount Reward { get; }

        public int RequiredProgress { get; }
        public IReadOnlyReactiveProperty<int> ProgressCounter { get; }
        public IReadOnlyReactiveProperty<string> ProgressStr => progressStr;

        public IReadOnlyReactiveProperty<bool> IsCompleted { get; }
        public IReadOnlyReactiveProperty<bool> IsRewarded { get; }

        public QuestVM(QuestInfo info, QuestsVMFactory questsVMF)
        {
            this.info = info;

            mechanicVM = questsVMF.GetMechanic(info.MechanicId);

            Id = info.Id;
            GroupId = info.GroupId;
            Reward = info.Reward;
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

            info.ProgressCounter
                .Subscribe(ProgressChangedCallback)
                .AddTo(this);
        }

        private void ProgressChangedCallback(int progress)
        {
            progressStr.Value = $"{progress} / {info.RequiredProgress}";
        }
    }
}