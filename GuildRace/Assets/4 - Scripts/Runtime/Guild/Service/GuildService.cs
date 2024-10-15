using AD.Services;
using AD.Services.Localization;
using AD.Services.ProtectedTime;
using Cysharp.Threading.Tasks;
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

        public ICharactersCollection Characters => state.Characters;
        public IGuildRanksCollection GuildRanks => state.GuildRanks;

        public IRecruitingModule RecruitingModule => recruitingModule;

        public GuildService(
            GuildConfig config,
            ILocalizationService localization,
            ITimeService time,
            IObjectResolver resolver)
        {
            state = new(config, localization, resolver);
            recruitingModule = new(config, state, time);
        }

        public override async UniTask<bool> Init()
        {
            state.Init();
            recruitingModule.Init();

            return await Inited();
        }

        // == Guild ==

        public void CreateOrUpdateGuild(GuildEM guildEM)
        {
            state.CreateOrUpdateGuild(guildEM);
        }

        public int RemoveCharacter(string characterId)
        {
            return state.RemoveCharacter(characterId);
        }

        public int AcceptJoinRequest(string requestId)
        {
            return state.AcceptJoinRequest(requestId);
        }

        public int DeclineJoinRequest(string requestId)
        {
            return state.RemoveJoinRequest(requestId);
        }

        public void SetClassWeightState(ClassId classId, bool isEnabled)
        {
            state.RecruitingState.SetClassWeightState(classId, isEnabled);
        }
    }
}