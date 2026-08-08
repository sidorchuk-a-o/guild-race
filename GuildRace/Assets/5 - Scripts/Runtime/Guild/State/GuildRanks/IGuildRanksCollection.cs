using AD.ToolsCollection;

namespace AD.Game.Guild
{
    public interface IGuildRanksCollection : IReadOnlyReactiveCollection<GuildRankInfo>
    {
        GuildRankInfo this[GuildRankId id] { get; }

        GuildRankInfo GuildMaster { get; }
    }
}