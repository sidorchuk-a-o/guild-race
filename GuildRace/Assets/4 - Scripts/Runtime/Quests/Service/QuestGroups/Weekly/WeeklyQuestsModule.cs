using AD.ToolsCollection;
using Game.Weekly;
using System.Linq;
using UniRx;
using VContainer;

namespace Game.Quests
{
    public class WeeklyQuestsModule : QuestsGroupModule
    {
        private WeeklyQuestsState state;
        private IWeeklyService weeklyService;

        public override IQuestsCollection Quests => state.Quests;

        [Inject]
        public void Inject(IWeeklyService weeklyService)
        {
            this.weeklyService = weeklyService;
        }

        public override void Init()
        {
            base.Init();

            state = new WeeklyQuestsState(questsConfig, weeklyService, resolver);
            state.Init();

            TryUpdateQuests();
        }

        private void TryUpdateQuests()
        {
            var currentWeek = weeklyService.CurrentWeek;
            var lastResetWeek = state.LastResetWeek;

            if (lastResetWeek == currentWeek)
            {
                return;
            }

            state.ClearQuests();

            var questsDataPool = questsConfig.Quests
                .Where(x => x.GroupId == Id)
                .RandomValues(MaxQuestsCount);

            var newQuests = questsDataPool.Select(data =>
            {
                var questId = GuidUtils.Generate();
                var quest = new QuestInfo(questId, data);

                return quest;
            });

            state.AddQuests(newQuests);
            state.SetResetWeek(currentWeek);
        }

        public override bool TakeQuestReward(TakeRewardArgs args)
        {
            var result = base.TakeQuestReward(args);

            if (result)
            {
                state.MarkAsDirty();
            }

            return result;
        }
    }
}