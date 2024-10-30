using UniRx;

namespace Game.Guild
{
    public interface IGuildService
    {
        bool GuildExists { get; }
        IReadOnlyReactiveProperty<string> Name { get; }

        ICharactersCollection Characters { get; }
        IGuildRanksCollection GuildRanks { get; }
        IGuildBankTabsCollection BankTabs { get; }

        IRecruitingModule RecruitingModule { get; }

        void CreateOrUpdateGuild(GuildEM guildEM);
        int RemoveCharacter(string characterId);
        int AcceptJoinRequest(string requestId);
        int DeclineJoinRequest(string requestId);
        void SetClassRoleSelectorState(RoleId roleId, bool isEnabled);
    }
}