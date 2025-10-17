using AD.Services.Analytics;

namespace Game.GuildLevels
{
    public static class GuildLevelsAnalyticsExtensions
    {
        public static void UnlockLevel(this IAnalyticsService analytics, LevelInfo level)
        {
            var parameters = AnalyticsParams.Default;
            parameters.AddKey(level.Level.ToString());

            analytics?.SendEvent("unlock_guild_level", parameters);
        }
    }
}