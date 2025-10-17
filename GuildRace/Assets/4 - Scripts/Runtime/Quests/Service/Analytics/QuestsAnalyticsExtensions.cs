using AD.Services.Analytics;
using AD.Services.Localization;
using System.Linq;

namespace Game.Quests
{
    public static class QuestsAnalyticsExtensions
    {
        private static IQuestsService QuestsService { get; set; }
        private static ILocalizationService Localization { get; set; }

        public static void Init(IQuestsService questsService, ILocalizationService localization)
        {
            QuestsService = questsService;
            Localization = localization;
        }

        public static void CompleteQuest(this IAnalyticsService analytics, string questId, QuestsGroupModule questGroup)
        {
            var groupName = questGroup.Title;
            var quest = questGroup.Quests.FirstOrDefault(x => x.Id == questId);
            var mechanic = QuestsService.GetMechanicHandler(quest.MechanicId);
            var mechanicNameData = mechanic.GetNameKey(quest);

            var questName = Localization.Get(
                mechanicNameData.LocalizeKey,
                languageCode: "ru",
                parameters: mechanicNameData.Data);

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