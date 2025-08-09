using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Game.Tutorial
{
    public interface ITutorialService
    {
        bool ContainerIsShowed(string containerId);
        void MarkContainerAsShowed(string containerId);
        UniTask ShowContainer(AssetReference containerRef);
    }
}