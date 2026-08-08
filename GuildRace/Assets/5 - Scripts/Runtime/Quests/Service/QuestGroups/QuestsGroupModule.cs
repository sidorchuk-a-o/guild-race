using System.Linq;
using AD.AppEvents;
using AD.Localization;
using AD.Store;
using AD.ToolsCollection;
using R3;
using UnityEngine;
using VContainer;

namespace AD.Game.Quests
{
    public abstract class QuestsGroupModule : ScriptableEntity<int>, IAppTickHandler
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
        public ReadOnlyReactiveProperty<int> MaxQuestsCount => maxQuestsCountProp;

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

            appEvents.AddAppTickHandler(this);
        }

        void IAppTickHandler.OnTick(float deltaTime)
        {
            if (CheckForUpdate())
            {
                UpdateQuests();
            }
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