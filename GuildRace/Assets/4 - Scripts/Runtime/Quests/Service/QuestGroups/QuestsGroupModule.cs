using AD.Services.Localization;
using AD.Services.Store;
using AD.ToolsCollection;
using System.Linq;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Quests
{
    public abstract class QuestsGroupModule : ScriptableEntity<int>
    {
        [SerializeField] private LocalizeKey nameKey;
        [SerializeField] private int maxQuestsCount;

        private readonly ReactiveProperty<int> maxQuestsCountProp = new();

        private IStoreService store;

        protected QuestsConfig questsConfig;
        protected IObjectResolver resolver;

        public QuestsGroup Group => Id;
        public LocalizeKey NameKey => nameKey;

        public int DefaultMaxQuestsCount => maxQuestsCount;
        public IReadOnlyReactiveProperty<int> MaxQuestsCount => maxQuestsCountProp;

        public abstract IQuestsCollection Quests { get; }

        [Inject]
        public void Inject(QuestsConfig questsConfig, IStoreService store, IObjectResolver resolver)
        {
            this.store = store;
            this.questsConfig = questsConfig;
            this.resolver = resolver;
        }

        public virtual void Init()
        {
            maxQuestsCountProp.Value = maxQuestsCount;
        }

        public void SetQuestsCount(int count)
        {
            var addCount = count - maxQuestsCountProp.Value;

            if (addCount > 0)
            {
                maxQuestsCountProp.Value = count;

                AddQuests(addCount);
            }
        }

        protected abstract void AddQuests(int count);

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