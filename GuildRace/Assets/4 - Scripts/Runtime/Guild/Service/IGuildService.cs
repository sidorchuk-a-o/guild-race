using UniRx;

namespace Game.Guild
{
    public interface IGuildService
    {
        bool GuildExists { get; }
        IReadOnlyReactiveProperty<string> GuildName { get; }
        IReadOnlyReactiveProperty<string> PlayerName { get; }
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

        void StateMarkAsDirty();
    }
}