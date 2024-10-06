using AD.Services;
using AD.Services.Router;
using Cysharp.Threading.Tasks;
using Game.Guild;

namespace Game
{
    public class GameService : Service, IGameService
    {
        private readonly IRouterService router;
        private readonly IGuildService guildService;

        public GameService(IRouterService router, IGuildService guildService)
        {
            this.router = router;
            this.guildService = guildService;
        }

        public override async UniTask<bool> Init()
        {
            return await Inited();
        }

        public async UniTask StartGame()
        {
            var pathKey = guildService.GuildExists
                ? RouteKeys.Hub.main
                : RouteKeys.Guild.create;

            await router.OpenScene(
                sceneKey: SceneKeys.hub,
                pathKey: pathKey,
                loadingKey: LoadingScreenKeys.startApp,
                parameters: RouteParams.FirstRoute);
        }
    }
}