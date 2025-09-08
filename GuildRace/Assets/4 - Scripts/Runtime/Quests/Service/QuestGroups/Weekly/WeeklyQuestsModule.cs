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

            state = new WeeklyQuestsState(questsConfig, resolver);
            state.Init();
        }

        protected override void TryUpdateQuests()
        {
            var currentWeek = weeklyService.CurrentWeek.Value;
            var lastResetWeek = state.LastResetWeek;

            if (lastResetWeek == currentWeek)
            {
                return;
            }

            state.ClearQuests();

            AddQuests(MaxQuestsCount.Value);

            state.SetResetWeek(currentWeek);
        }

        protected override void AddQuests(int count)
        {
            var questsDataPool = questsConfig.Quests
                .Where(x => x.GroupId == Id)
                .RandomValues(count);

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