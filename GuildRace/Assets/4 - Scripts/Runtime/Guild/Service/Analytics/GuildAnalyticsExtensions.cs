using AD.Services.Analytics;
using AD.Services.Localization;
using System.Linq;

namespace Game.Guild
{
    public static class GuildAnalyticsExtensions
    {
        private static GuildConfig Config { get; set; }
        private static ILocalizationService Localization { get; set; }

        public static void Init(GuildConfig config, ILocalizationService localization)
        {
            Config = config;
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
            var parameters = AnalyticsParams.Default;
            parameters.AddCharacter(character);
            parameters.AddGuildRank(character);

            analytics?.SendEvent("guild_rank_changed", parameters);
        }

        public static void AddCharacter(this AnalyticsParams parameters, CharacterInfo character)
        {
            var charactersParams = Config.CharactersParams;

            parameters["character_class"] = charactersParams.GetClass(character.ClassId).Title;
            parameters["character_spec"] = charactersParams.GetSpecialization(character.SpecId).Title;
            parameters["items_level"] = character.ItemsLevel.Value;
        }

        public static void AddGuildRank(this AnalyticsParams parameters, CharacterInfo character)
        {
            var guildRank = character.GuildRankId.Value;
            var guildRanks = Config.DefaultGuildRanks;

            var rankData = guildRanks.FirstOrDefault(x => x.Id == guildRank);
            var rankName = Localization.Get(rankData.DefaultNameKey, languageCode: "ru");

            parameters["guild_rank"] = rankName;
        }
    }
}