using AD.ToolsCollection;
using AD.Yandex;
using Cysharp.Threading.Tasks;
using VContainer;

namespace AD.Game
{
    public class AppEntryPoint : Management.AppEntryPoint
    {
        private IGameService gameService;
        private IYandexGameService yandexService;

        [Inject]
        public void Inject(IGameService gameService, IYandexGameService yandexService)
        {
            this.gameService = gameService;
            this.yandexService = yandexService;
        }

        public override UniTask<bool> InitGame()
        {
            return UniTask.FromResult(true);
        }

        public override async UniTask LaunchGame()
        {
            this.LogMsg("Launch Game");

            await gameService.StartGame();

            yandexService.GameReady();

            this.LogMsg("Game Launched");
        }
    }
}