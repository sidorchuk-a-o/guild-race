using AD.Services.Analytics;
using AD.Services.Localization;
using System.Linq;

namespace Game.Guild
{
    public static class GuildAnalyticsExtensions
    {
        private static GuildConfig Config { get; set; }
        private static IGuildService GuildService { get; set; }
        private static ILocalizationService Localization { get; set; }

        public static void Init(GuildConfig config, IGuildService guildService, ILocalizationService localization)
        {
            Config = config;
            GuildService = guildService;
            Localization = localization;
        }

        public static void AcceptJoinRequest(this IAnalyticsService analytics, CharacterInfo character)
        {
            var parameters = AnalyticsParams.Default;
            parameters.AddCharacter(character);

            analytics?.SendEvent("accept_join_request", parameters);
        }

        public static void DeclineJoinRequest(this IAnalyticsService analytics, CharacterInfo character)
        {
            var parameters = AnalyticsParams.Default;
            parameters.AddCharacter(character);

            analytics?.SendEvent("decline_join_request", parameters);
        }

        public static void RemoveCharacter(this IAnalyticsService analytics, CharacterInfo character)
        {
            var parameters = AnalyticsParams.Default;
            parameters.AddCharacter(character);

            analytics?.SendEvent("remove_character", parameters);
        }

        public static void GuildRankChanged(this IAnalyticsService analytics, CharacterInfo character)
        {
            var rankParams = AnalyticsParams.Default;
            var characterParams = AnalyticsParams.Default;

            characterParams.AddCharacter(character.Id);

            var guildRankId = character.GuildRankId.Value;
            var guildRanks = Config.DefaultGuildRanks;
            var rankData = guildRanks.FirstOrDefault(x => x.Id == guildRankId);
            var rankName = Localization.Get(rankData.DefaultNameKey, languageCode: "ru");

            rankParams[rankName] = characterParams;

            analytics?.SendEvent("guild_rank_changed", rankParams);
        }

        public static void AddCharacter(this AnalyticsParams parameters, string characterId)
        {
            var character = GuildService.Characters[characterId];

            parameters.AddCharacter(character);
        }

        private static void AddCharacter(this AnalyticsParams parameters, CharacterInfo character)
        {
            var charactersParams = Config.CharactersParams;

            var classTitle = charactersParams.GetClass(character.ClassId).Title;
            var specTitle = charactersParams.GetSpecialization(character.SpecId).Title;

            parameters[$"{classTitle} - {specTitle}"] = character.ItemsLevel.Value;
        }
    }
}