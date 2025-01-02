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
            await router.ShowLoading(LoadingScreenKeys.startApp);

            await router.OpenScene(
                sceneKey: SceneKeys.game,
                parameters: RouteParams.FirstRoute);

            if (instancesService.HasPlayerInstance)
            {
                await instancesService.StartPlayerInstance();
            }
            else
            {
                await router.PushAsync(
                    pathKey: getPathKey(),
                    parameters: RouteParams.FirstRoute);

                RouteKey getPathKey()
                {
                    if (guildService.GuildExists == false)
                    {
                        return RouteKeys.Guild.createGuild;
                    }

                    return RouteKeys.Hub.roster;
                }
            }

            await router.HideLoading(LoadingScreenKeys.startApp);
        }
    }
}