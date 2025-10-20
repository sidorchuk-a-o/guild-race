using AD.Services;
using AD.Services.Analytics;
using AD.Services.AppEvents;
using AD.Services.Leaderboards;
using AD.Services.Localization;
using AD.Services.ProtectedTime;
using System.Linq;
using Cysharp.Threading.Tasks;
using Game.GuildLevels;
using Game.Inventory;
using VContainer;
using UniRx;

namespace Game.Guild
{
    public class GuildService : Service, IGuildService
    {
        private readonly GuildState state;
        private readonly IAnalyticsService analytics;

        private readonly RecruitingModule recruitingModule;
        private readonly GuildLeaderboardModule leaderboardModule;

        public bool GuildExists => state.IsExists;
        public IReadOnlyReactiveProperty<string> GuildName => state.GuildName;
        public IReadOnlyReactiveProperty<string> PlayerName => state.PlayerName;
        public IReadOnlyReactiveProperty<int> MaxCharactersCount => state.MaxCharactersCount;
        public EmblemInfo Emblem => state.Emblem;

        public ICharactersCollection Characters => state.Characters;
        public IGuildRanksCollection GuildRanks => state.GuildRanks;
        public IGuildBankTabsCollection BankTabs => state.BankTabs;

        public IRecruitingModule RecruitingModule => recruitingModule;

        public GuildService(
            GuildConfig guildConfig,
            InventoryConfig inventoryConfig,
            IGuildLevelsService guildLevelsService,
            IInventoryService inventoryService,
            ILeaderboardsService leaderboards,
            ILocalizationService localization,
            ITimeService time,
            IAppEventsService appEvents,
            IAnalyticsService analytics,
            IObjectResolver resolver)
        {
            this.analytics = analytics;

            state = new(guildConfig, guildLevelsService, inventoryService, resolver);
            recruitingModule = new(state, guildConfig, inventoryConfig, inventoryService, localization, time, resolver);
            leaderboardModule = new(state, guildConfig, leaderboards, appEvents);

            GuildAnalyticsExtensions.Init(guildConfig, this, localization);
        }

        public override async UniTask<bool> Init()
        {
            state.Init();
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
            if (GuildExists == false)
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

        public string CreateLeaderboeardExtraData()
        {
            return state.CreateLeaderboeardExtraData();
        }

        public GuildScoreData GetLeaderboeardExtraData(string json)
        {
            return state.GetLeaderboeardExtraData(json);
        }
    }
}