using AD.Services.Localization;
using AD.Services.Store;
using AD.ToolsCollection;
using System.Linq;
using UnityEngine;
using VContainer;

namespace Game.Quests
{
    public abstract class QuestsGroupModule : ScriptableEntity<int>
    {
        [SerializeField] private LocalizeKey nameKey;
        [SerializeField] private int maxQuestsCount;

        private IStoreService store;

        protected QuestsConfig questsConfig;
        protected IObjectResolver resolver;

        public LocalizeKey NameKey => nameKey;
        public int MaxQuestsCount => maxQuestsCount;

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
        }

        public virtual bool TakeQuestReward(TakeRewardArgs args)
        {
            var quest = Quests.FirstOrDefault(x => x.Id == args.QuestId);

            if (quest == null)
            {
                return false;
            }

            quest.MarkAsRewarded();

            store.CurrenciesModule.AddCurrency(quest.Reward);

            return true;
        }
    }
}