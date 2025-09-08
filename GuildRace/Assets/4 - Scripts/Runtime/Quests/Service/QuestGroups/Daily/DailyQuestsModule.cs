using AD.ToolsCollection;
using System;
using System.Linq;
using UniRx;

namespace Game.Quests
{
    public class DailyQuestsModule : QuestsGroupModule
    {
        private DailyQuestsState state;

        public override IQuestsCollection Quests => state.Quests;

        public override void Init()
        {
            base.Init();

            state = new DailyQuestsState(questsConfig, resolver);
            state.Init();
        }

        protected override void TryUpdateQuests()
        {
            var currentDate = DateTime.Today;
            var lastResetData = state.LastResetDate;
            var daysDelta = (currentDate - lastResetData).TotalDays;

            if (daysDelta <= 0)
            {
                return;
            }

            state.ClearQuests();

            AddQuests(MaxQuestsCount.Value);

            state.SetLastResetDate(currentDate);
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