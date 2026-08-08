using System.Linq;
using AD.Game.Weekly;
using AD.ToolsCollection;
using VContainer;

namespace AD.Game.Quests
{
    public class WeeklyQuestsModule : QuestsGroupModule
    {
        private WeeklyQuestsState state;
        private IWeeklyService weeklyService;

        public override IQuestsCollection Quests => state.Quests;

        [Inject]
        public void Inject(WeeklyQuestsState state, IWeeklyService weeklyService)
        {
            this.state = state;
            this.weeklyService = weeklyService;
        }

        protected override bool CheckForUpdate()
        {
            var currentWeek = weeklyService.CurrentWeek.CurrentValue;
            var lastResetWeek = state.LastResetWeek;

            return lastResetWeek != currentWeek;
        }

        public override void UpdateQuests()
        {
            state.ClearQuests();

            AddQuests(MaxQuestsCount.CurrentValue);

            state.SetResetWeek(weeklyService.CurrentWeek.CurrentValue);
        }

        protected override void AddQuests(int count)
        {
            var questsDataPool = questsConfig
                .GetQuestsByGroup(Group)
                .Shuffle()
                .Take(count);

            var newQuests = questsDataPool.Select(data =>
            {
                var questId = GuidUtils.Generate();
                var quest = new QuestInfo(questId, data);

                return quest;
            });

            state.AddQuests(newQuests);
        }

        public override bool TakeQuestReward(TakeRewardArgs args, float bonusValue)
        {
            var result = base.TakeQuestReward(args, bonusValue);

            if (result)
            {
                state.MarkAsDirty(true);
            }

            return result;
        }
    }
}