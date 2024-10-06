using AD.Services;
using Cysharp.Threading.Tasks;
using UniRx;
using VContainer;

namespace Game.Guild
{
    public class GuildService : Service, IGuildService
    {
        private readonly GuildState state;

        public bool GuildExists => state.IsExists;
        public IReadOnlyReactiveProperty<string> Name => state.Name;

        public GuildService(GuildConfig config, IObjectResolver resolver)
        {
            state = new(config, resolver);
        }

        public override async UniTask<bool> Init()
        {
            state.Init();

            return await Inited();
        }

        // == Guild ==

        public void CreateGuild(GuildEM guildEM)
        {
            state.CreateGuild(guildEM);
        }
    }
}