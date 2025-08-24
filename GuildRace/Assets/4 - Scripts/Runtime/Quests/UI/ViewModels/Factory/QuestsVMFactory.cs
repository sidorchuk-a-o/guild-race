using AD.Services.Router;
using AD.Services.Store;
using UniRx;
using VContainer;

namespace Game.Quests
{
    public class QuestsVMFactory : VMFactory
    {
        private IQuestsService questsService;
        private IObjectResolver resolver;

        private StoreVMFactory storeVMF;

        public IReadOnlyReactiveProperty<float> RewardBonus => questsService.RewardBonus;
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

            return new QuestsVM(module.Quests, this);
        }

        public QuestMechanicVM GetMechanic(int mechanicId)
        {
            var mechanic = questsService.GetMechanicHandler(mechanicId);

            return new QuestMechanicVM(mechanic);
        }

        public CompletedQuestsVM GetCompletedQuests()
        {
            return new CompletedQuestsVM(questsService);
        }

        public void TakeQuestReward(TakeRewardArgs args)
        {
            questsService.TakeQuestReward(args);
        }
    }
}