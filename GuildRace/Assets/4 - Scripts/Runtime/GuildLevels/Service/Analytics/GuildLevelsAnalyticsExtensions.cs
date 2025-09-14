using AD.Services.Analytics;
using AD.Services.Store;

namespace Game.GuildLevels
{
    public static class GuildLevelsAnalyticsExtensions
    {
        public static void UnlockLevel(this IAnalyticsService analytics, LevelInfo level)
        {
            var parameters = AnalyticsParams.Default;
            parameters.AddCurrencyAmount(level.UnlockPrice.Key);
            parameters["price"] = level.UnlockPrice.Value.ToString();
            parameters["level"] = level.Level;

            analytics?.SendEvent("unlock_guild_level", parameters);
        }
    }
}