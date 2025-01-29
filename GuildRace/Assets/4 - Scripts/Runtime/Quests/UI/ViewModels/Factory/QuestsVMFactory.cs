using AD.Services.Router;
using VContainer;

namespace Game.Quests
{
    public class QuestsVMFactory : VMFactory
    {
        private IQuestsService questsService;

        [Inject]
        public void Inject(IQuestsService questsService)
        {
            this.questsService = questsService;
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