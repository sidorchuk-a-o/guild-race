using System.Linq;
using AD.Management;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace AD.Game.Tutorial
{
    public class TutorialService : Service, ITutorialService
    {
        private readonly TutorialState state;

        private TutorialUIModule uiModule;

        public TutorialService(TutorialState state)
        {
            this.state = state;
        }

        public override async UniTask<bool> Init()
        {
            uiModule = ResolveModule<TutorialUIModule>();
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

        public void ResetProgress()
        {
            state.ResetProgress();
        }

        public override void Dispose()
        {
            base.Dispose();

            uiModule.Dispose();
        }
    }
}