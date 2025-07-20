using System;
using System.Linq;
using AD.Services;
using AD.Services.Router;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using VContainer;

namespace Game.Tutorial
{
    public class TutorialService : Service, ITutorialService
    {
        private readonly TutorialState state;
        private readonly TutorialUIModule uiModule;

        public TutorialService(IRouterService router, IObjectResolver resolver)
        {
            state = new(resolver);
            uiModule = new(router);
        }

        public override async UniTask<bool> Init()
        {
            state.Init();
            uiModule.Init();

            return await Inited();
        }

        public bool ContainerIsShowed(string containerId)
        {
            return state.ShowedComponents.Contains(containerId);
        }

        public async void MarkContainerAsShowed(string containerId)
        {
            state.AddComponent(containerId);

            await uiModule.HideContainer();
        }

        public async UniTask ShowContainer(AssetReference containerRef)
        {
            await uiModule.ShowContainer(containerRef);
        }

        public override void Dispose()
        {
            base.Dispose();

            uiModule.Dispose();
        }
    }
}