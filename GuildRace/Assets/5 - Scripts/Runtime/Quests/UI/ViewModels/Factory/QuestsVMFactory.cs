using AD.Router;
using AD.Store;
using R3;
using VContainer;

namespace AD.Game.Quests
{
    public class QuestsVMFactory : VMFactory
    {
        private IQuestsService questsService;
        private IObjectResolver resolver;

        private StoreVMFactory storeVMF;

        public ReadOnlyReactiveProperty<float> RewardBonus => questsService.RewardBonus;
        public StoreVMFactory StoreVMF => storeVMF ??= resolver.Resolve<StoreVMFactory>();

        [Inject]
        public void Inject(IQuestsService questsService, IObjectResolver resolver)
        {
            this.questsService = questsService;
            this.resolver = resolver;
        }

        public QuestsVM GetQuests(QuestsGroup group)
        {
            var module = questsService.GetGroupModule(group);

            return new(module.Quests, this);
        }

        public QuestMechanicVM GetMechanic(int mechanicId)
        {
            var mechanic = questsService.GetMechanicHandler(mechanicId);

            return new(mechanic);
        }

        public CompletedQuestsVM GetCompletedQuests()
        {
            return new(questsService);
        }

        public void TakeQuestReward(TakeRewardArgs args)
        {
            questsService.TakeQuestReward(args);
        }
    }
}