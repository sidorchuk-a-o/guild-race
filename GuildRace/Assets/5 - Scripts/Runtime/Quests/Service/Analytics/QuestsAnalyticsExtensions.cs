using System.Linq;
using AD.Analytics;
using AD.Localization;

namespace AD.Game.Quests
{
    public static class QuestsAnalyticsExtensions
    {
        private static IQuestsService QuestsService { get; set; }

        public static void Init(IQuestsService questsService)
        {
            QuestsService = questsService;
        }

        public static void CompleteQuest(this IAnalyticsService analytics, string questId, QuestsGroupModule questGroup)
        {
            var quest = questGroup.Quests.First(x => x.Id == questId);
            var mechanic = QuestsService.GetMechanicHandler(quest.MechanicId);
            var mechanicNameData = mechanic.GetNameKey(quest);

            var questName = (LocalizeText)mechanicNameData.LocalizeKey;
            questName.SetParameters(mechanicNameData.Data);
            questName.LanguageCode = "ru";

            // params
            var questParams = AnalyticsParams.Empty;
            questParams["quest_id"] = quest.DataId;

            var groupParams = AnalyticsParams.Empty;
            groupParams["group_id"] = questGroup.Id;
            groupParams[questName] = questParams;

            var parameters = AnalyticsParams.Default;
            parameters[questGroup.Title] = groupParams;

            analytics?.SendEvent("complete_quest", parameters);
        }
    }
}