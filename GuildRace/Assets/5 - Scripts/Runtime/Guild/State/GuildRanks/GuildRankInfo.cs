using AD.Localization;

namespace AD.Game.Guild
{
    public class GuildRankInfo
    {
        public GuildRankId Id { get; }
        public LocalizeKey NameKey { get; }

        public GuildRankInfo(GuildRankData data)
        {
            Id = data.Id;
            NameKey = data.DefaultNameKey;
        }
    }
}