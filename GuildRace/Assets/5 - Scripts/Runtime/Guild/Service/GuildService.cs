using System.Linq;
using AD.Analytics;
using AD.Management;
using Cysharp.Threading.Tasks;
using R3;

namespace AD.Game.Guild
{
    public class GuildService : Service, IGuildService
    {
        private readonly GuildState state;
        private readonly IAnalyticsService analytics;

        private RecruitingModule recruitingModule;
        private GuildLeaderboardModule leaderboardModule;

        public bool GuildExists => state.IsExists;
        public ReadOnlyReactiveProperty<string> GuildName => state.GuildName;
        public ReadOnlyReactiveProperty<string> PlayerName => state.PlayerName;
        public ReadOnlyReactiveProperty<int> MaxCharactersCount => state.MaxCharactersCount;
        public EmblemInfo Emblem => state.Emblem;

        public ICharactersCollection Characters => state.Characters;
        public IGuildRanksCollection GuildRanks => state.GuildRanks;
        public IGuildBankTabsCollection BankTabs => state.BankTabs;

        public IRecruitingModule RecruitingModule => recruitingModule;

        public GuildService(GuildState state, GuildConfig guildConfig)
        {
            this.state = state;

            GuildAnalyticsExtensions.Init(guildConfig, this);
        }

        public override async UniTask<bool> Init()
        {
            recruitingModule = ResolveModule<RecruitingModule>();
            leaderboardModule = ResolveModule<GuildLeaderboardModule>();

            recruitingModule.Init();
            leaderboardModule.Init();

            return await Inited();
        }

        public void StateMarkAsDirty()
        {
            state.MarkAsDirty();
        }

        // == Guild ==

        public void CreateOrUpdateGuild(GuildEM guildEM)
        {
            if (!GuildExists)
            {
                recruitingModule.SwitchRecruitingState();

                analytics.CreateGuild(guildEM);
            }

            state.CreateOrUpdateGuild(guildEM);
        }

        // == Emblem ==

        public EmblemEM CreateEmblemEM()
        {
            return state.CreateEmblemEM();
        }

        public void SaveEmblem(EmblemEM emblemEM)
        {
            state.SaveEmblem(emblemEM);
        }

        // == Characters ==

        public int RemoveCharacter(string characterId)
        {
            var character = state.Characters.FirstOrDefault(x => x.Id == characterId);
            var index = state.RemoveCharacter(characterId);

            if (character != null)
            {
                analytics.RemoveCharacter(character);
            }

            return index;
        }

        public int AcceptJoinRequest(string requestId)
        {
            var index = recruitingModule.AcceptJoinRequest(requestId, out var request);

            if (request != null)
            {
                analytics.AcceptJoinRequest(request.Character);
            }

            return index;
        }

        public int DeclineJoinRequest(string requestId)
        {
            var index = recruitingModule.RemoveRequest(requestId, out var request);

            if (request != null)
            {
                analytics.DeclineJoinRequest(request.Character);
            }

            return index;
        }

        public void SetClassRoleSelectorState(RoleId roleId, bool isEnabled)
        {
            recruitingModule.SetClassRoleSelectorState(roleId, isEnabled);
        }

        public void UpdateGuildRank(string characterId, int rankIndex)
        {
            var character = state.Characters[id: characterId];
            var guildRank = state.GuildRanks[index: rankIndex];

            character.SetGuildRank(guildRank.Id);

            state.UpdateOrderByRank(character);

            analytics.GuildRankChanged(character);
        }

        // == Leaderboard ==

        public string CreateLeaderboardExtraData()
        {
            return state.CreateLeaderboardExtraData();
        }

        public GuildScoreData GetLeaderboardExtraData(string json)
        {
            return GuildState.GetLeaderboardExtraData(json);
        }
    }
}