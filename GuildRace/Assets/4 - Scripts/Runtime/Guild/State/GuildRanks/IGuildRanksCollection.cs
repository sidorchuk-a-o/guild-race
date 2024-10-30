using AD.States;

namespace Game.Guild
{
    public interface IGuildRanksCollection : IReadOnlyReactiveCollectionInfo<GuildRankInfo>
    {
        GuildRankInfo this[GuildRankId id] { get; }

        GuildRankInfo GuildMaster { get; }
    }
}