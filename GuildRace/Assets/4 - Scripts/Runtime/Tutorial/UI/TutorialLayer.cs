using AD.Services.Router;
using AD.ToolsCollection;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Game.Tutorial
{
    public class TutorialLayer : RouterLayerContainer
    {
        private readonly Key<string> tutorialKey = new("tutorial");

        public async UniTask<TutorialContainer> GetContainer(AssetReference containerRef)
        {
            var rootKey = GetRootKey(tutorialKey);

            var containerId = containerRef.AssetGUID;
            var containerKey = new Key<string>(containerId);

            var container = await GetOrCreateContent(
                contentKey: containerKey,
                contentRef: containerRef,
                rootKey: rootKey);

            if (container == null)
            {
                return null;
            }

            var rootCanvas = await ShowRootCanvas(rootKey);

            var tutorialContainer = container.GetComponent<TutorialContainer>();
            var tapToCloseContainer = rootCanvas.GetComponent<TapToCloseContainer>();

            tutorialContainer.SetId(containerId);
            tapToCloseContainer.SetContainerId(containerId);

            return tutorialContainer;
        }

        public async UniTask HideContainer()
        {
            var rootKey = GetRootKey(tutorialKey);

            await HideRootCanvas(rootKey, deactivate: true);
        }
    }
}