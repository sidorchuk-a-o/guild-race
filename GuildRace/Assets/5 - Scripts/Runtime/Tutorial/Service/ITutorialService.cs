using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace AD.Game.Tutorial
{
    public interface ITutorialService
    {
        bool ContainerIsShowed(string containerId);
        void MarkContainerAsShowed(string containerId);
        UniTask ShowContainer(AssetReference containerRef);
        void ResetProgress();
    }
}