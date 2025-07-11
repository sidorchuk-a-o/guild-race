﻿using AD.ToolsCollection;
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

            TryUpdateQuests();
        }

        private void TryUpdateQuests()
        {
            var currentDate = DateTime.Today;
            var lastResetData = state.LastResetDate;
            var daysDelta = (currentDate - lastResetData).TotalDays;

            if (daysDelta <= 0)
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
            state.SetLastResetDate(currentDate);
        }

        public override bool TakeQuestReward(TakeRewardArgs args)
        {
            var result = base.TakeQuestReward(args);

            if (result)
            {
                state.MarkAsDirty(true);
            }

            return result;
        }
    }
}