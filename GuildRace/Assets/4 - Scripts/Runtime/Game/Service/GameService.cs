using AD.Services;
using AD.Services.Router;
using Cysharp.Threading.Tasks;

namespace Game
{
    public class GameService : Service, IGameService
    {
        private readonly IRouterService router;

        public GameService(IRouterService router)
        {
            this.router = router;
        }

        public override async UniTask<bool> Init()
        {
            return await Inited();
        }

        public async UniTask StartGame()
        {
            await StartHub(startApp: true);
        }

        // == Hub ==

        public async UniTask StartHub(bool startApp)
        {
            var loadingKey = startApp
                ? LoadingScreenKeys.startApp
                : LoadingScreenKeys.loading;

            await router.OpenScene(
                sceneKey: SceneKeys.hub,
                pathKey: RouteKeys.Hub.main,
                loadingKey: loadingKey,
                parameters: RouteParams.FirstRoute);
        }
    }
}