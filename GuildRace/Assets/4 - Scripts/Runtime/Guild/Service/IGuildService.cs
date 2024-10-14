using UniRx;

namespace Game.Guild
{
    public interface IGuildService
    {
        bool GuildExists { get; }
        IReadOnlyReactiveProperty<string> Name { get; }

        ICharactersCollection Characters { get; }
        IGuildRanksCollection GuildRanks { get; }

        IRecruitingModule RecruitingModule { get; }

        void CreateOrUpdateGuild(GuildEM guildEM);
        int RemoveCharacter(string characterId);
        int AcceptJoinRequest(string requestId);
        int RemoveJoinRequest(string requestId);
        void SetClassWeightState(ClassId classId, bool isEnabled);
    }
}