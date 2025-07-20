using System;
using AD.Services.Router;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Game.Tutorial
{
    public class TutorialUIModule : IDisposable
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

        public async void Dispose()
        {
            await HideContainer();
        }
    }
}