using AD.Management;
using AD.Router;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace AD.Game.Tutorial
{
    public class TutorialUIModule : ServiceModule
    {
        private readonly IRouterService router;

        private TutorialLayer tutorialLayer;

        public TutorialUIModule(IRouterService router)
        {
            this.router = router;
        }

        public void Init()
        {
            tutorialLayer = router.GetLayer<TutorialLayer>(RouterLayerKeys.Tutorial);
        }

        public void SetBackPressState(bool state)
        {
            router.SetBackPressState(state, this);
        }

        public async UniTask ShowContainer(AssetReference containerRef)
        {
            await tutorialLayer.GetContainer(containerRef);
        }

        public async UniTask HideContainer()
        {
            await tutorialLayer.HideContainer();
        }

        public override void Dispose()
        {
            base.Dispose();

            HideContainer().Forget();
        }
    }
}