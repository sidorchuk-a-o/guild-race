using AD.Services;
using AD.Services.Router;
using Cysharp.Threading.Tasks;
using Game.Guild;
using Game.Instances;

namespace Game
{
    public class GameService : Service, IGameService
    {
        private readonly IRouterService router;

        private readonly IGuildService guildService;
        private readonly IInstancesService instancesService;

        public GameService(IRouterService router, IGuildService guildService, IInstancesService instancesService)
        {
            this.router = router;
            this.guildService = guildService;
            this.instancesService = instancesService;
        }

        public override async UniTask<bool> Init()
        {
            return await Inited();
        }

        public async UniTask StartGame()
        {
            var pathKey = !guildService.GuildExists
                ? RouteKeys.Guild.CreateGuild
                : RouteKeys.Hub.Roster;

            await router.OpenScene(
                sceneKey: SceneKeys.Game,
                loadingKey: LoadingScreenKeys.StartApp,
                pathKey: pathKey,
                parameters: RouteParams.FirstRoute);
        }
    }
}