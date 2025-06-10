using AD.Services;
using AD.Services.Localization;
using AD.Services.ProtectedTime;
using Cysharp.Threading.Tasks;
using Game.Inventory;
using UniRx;
using VContainer;

namespace Game.Guild
{
    public class GuildService : Service, IGuildService
    {
        private readonly GuildState state;
        private readonly RecruitingModule recruitingModule;

        public bool GuildExists => state.IsExists;
        public IReadOnlyReactiveProperty<string> Name => state.Name;
        public EmblemInfo Emblem => state.Emblem;

        public ICharactersCollection Characters => state.Characters;
        public IGuildRanksCollection GuildRanks => state.GuildRanks;
        public IGuildBankTabsCollection BankTabs => state.BankTabs;

        public IRecruitingModule RecruitingModule => recruitingModule;

        public GuildService(
            GuildConfig guildConfig,
            InventoryConfig inventoryConfig,
            IInventoryService inventoryService,
            ILocalizationService localization,
            ITimeService time,
            IObjectResolver resolver)
        {
            state = new(guildConfig, inventoryService, localization, resolver);
            recruitingModule = new(state, guildConfig, inventoryConfig, inventoryService, time, resolver);
        }

        public override async UniTask<bool> Init()
        {
            state.Init();
            recruitingModule.Init();

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
            return state.RemoveCharacter(characterId);
        }

        public int AcceptJoinRequest(string requestId)
        {
            var index = recruitingModule.AcceptJoinRequest(requestId, out var requestInfo);

            if (index != -1)
            {
                state.AddCharacter(requestInfo.Character);
            }

            return index;
        }

        public int DeclineJoinRequest(string requestId)
        {
            return recruitingModule.RemoveRequest(requestId);
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
        }
    }
}