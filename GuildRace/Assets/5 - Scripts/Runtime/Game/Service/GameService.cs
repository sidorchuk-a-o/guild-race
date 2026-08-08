using AD.Game.Guild;
using AD.Management;
using AD.Router;
using Cysharp.Threading.Tasks;

namespace AD.Game
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

        public override UniTask<bool> Init()
        {
            return Inited();
        }

        public async UniTask StartGame()
        {
            var pathKey = !guildService.GuildExists
                ? RouteKeys.Guild.CreateGuild
                : RouteKeys.Hub.Roster;

            var loadingKey = !guildService.GuildExists
                ? LoadingScreenKeys.StartApp
                : LoadingScreenKeys.Loading;

            await router.OpenScene(
                sceneKey: SceneKeys.Game,
                loadingKey: loadingKey,
                pathKey: pathKey,
                parameters: RouteParams.FirstRoute);
        }
    }
}