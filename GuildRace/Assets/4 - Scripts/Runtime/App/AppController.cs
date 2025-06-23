#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using AD.Yandex;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;

namespace Game
{
    public class AppController : AD.App.AppController
    {
        protected override async UniTask<bool> InitGame()
        {
            return true;
        }

        protected override async UniTask LaunchGame()
        {
            this.LogMsg("Launch Game");

            await AppScope.Resolve<IGameService>().StartGame();

            AppScope.Resolve<IYandexGameService>().GameReady();

            this.LogMsg("Game Launched");
        }
    }
}