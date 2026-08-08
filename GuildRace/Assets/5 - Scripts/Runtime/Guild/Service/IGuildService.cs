using R3;

namespace AD.Game.Guild
{
    public interface IGuildService
    {
        bool GuildExists { get; }
        ReadOnlyReactiveProperty<string> GuildName { get; }
        ReadOnlyReactiveProperty<string> PlayerName { get; }
        ReadOnlyReactiveProperty<int> MaxCharactersCount { get; }
        EmblemInfo Emblem { get; }

        ICharactersCollection Characters { get; }
        IGuildRanksCollection GuildRanks { get; }
        IGuildBankTabsCollection BankTabs { get; }

        IRecruitingModule RecruitingModule { get; }

        void CreateOrUpdateGuild(GuildEM guildEM);

        EmblemEM CreateEmblemEM();
        void SaveEmblem(EmblemEM emblemEM);

        int RemoveCharacter(string characterId);
        int AcceptJoinRequest(string requestId);
        int DeclineJoinRequest(string requestId);
        void SetClassRoleSelectorState(RoleId roleId, bool isEnabled);
        void UpdateGuildRank(string characterId, int rankIndex);

        string CreateLeaderboardExtraData();
        GuildScoreData GetLeaderboardExtraData(string json);

        void StateMarkAsDirty();
    }
}