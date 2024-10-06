using Cysharp.Threading.Tasks;

namespace Game
{
    public interface IGameService
    {
        UniTask StartGame();
        UniTask StartHub(bool startApp);
    }
}