using AD.Services.Store;
using AD.Services.AppEvents;
using AD.Services.Localization;
using AD.ToolsCollection;
using System.Linq;
using UnityEngine;
using VContainer;
using UniRx;

namespace Game.Quests
{
    public abstract class QuestsGroupModule : ScriptableEntity<int>, IAppTickListener
    {
        [SerializeField] private LocalizeKey nameKey;
        [SerializeField] private int maxQuestsCount;

        private readonly ReactiveProperty<int> maxQuestsCountProp = new();

        private IStoreService store;
        private IAppEventsService appEvents;

        protected QuestsConfig questsConfig;
        protected IObjectResolver resolver;

        public QuestsGroup Group => Id;
        public LocalizeKey NameKey => nameKey;

        public int DefaultMaxQuestsCount => maxQuestsCount;
        public IReadOnlyReactiveProperty<int> MaxQuestsCount => maxQuestsCountProp;

        public abstract IQuestsCollection Quests { get; }

        [Inject]
        public void Inject(
            QuestsConfig questsConfig,
            IStoreService store,
            IAppEventsService appEvents,
            IObjectResolver resolver)
        {
            this.store = store;
            this.appEvents = appEvents;

            this.questsConfig = questsConfig;
            this.resolver = resolver;
        }

        public virtual void Init()
        {
            maxQuestsCountProp.Value = maxQuestsCount;

            appEvents.AddAppTickListener(this);
        }

        void IAppTickListener.OnTick(float deltaTime)
        {
            if (CheckForUpdate())
            {
                UpdateQuests();
            }
        }

        void IAppTickListener.OnLateTick(float deltaTime)
        {
        }

        protected abstract bool CheckForUpdate();
        public abstract void UpdateQuests();
        protected abstract void AddQuests(int count);

        public void SetQuestsCount(int count)
        {
            var addCount = count - maxQuestsCountProp.Value;

            if (addCount > 0)
            {
                maxQuestsCountProp.Value = count;
            }

            if (Quests.Count < maxQuestsCountProp.Value)
            {
                addCount = maxQuestsCountProp.Value - Quests.Count;

                AddQuests(addCount);
            }
        }

        public virtual bool TakeQuestReward(TakeRewardArgs args, float bonusValue)
        {
            var quest = Quests.FirstOrDefault(x => x.Id == args.QuestId);

            if (quest == null)
            {
                return false;
            }

            quest.MarkAsRewarded();

            var reward = quest.Reward;
            var bonus = reward * bonusValue;

            store.CurrenciesModule.AddCurrency(reward + bonus);

            return true;
        }
    }
}