using System;
using System.Linq;
using AD.ToolsCollection;
using VContainer;

namespace AD.Game.Quests
{
    public class DailyQuestsModule : QuestsGroupModule
    {
        private DailyQuestsState state;

        public override IQuestsCollection Quests => state.Quests;

        [Inject]
        public void Inject(DailyQuestsState state)
        {
            this.state = state;
        }

        protected override bool CheckForUpdate()
        {
            var currentDate = DateTime.Today;
            var lastResetData = state.LastResetDate;
            var daysDelta = (currentDate - lastResetData).TotalDays;

            return daysDelta > 0;
        }

        public override void UpdateQuests()
        {
            state.ClearQuests();

            AddQuests(MaxQuestsCount.CurrentValue);

            state.SetLastResetDate(DateTime.Today);
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