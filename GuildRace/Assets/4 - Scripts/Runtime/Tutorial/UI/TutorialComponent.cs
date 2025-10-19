using AD.UI;
using AD.Services.Router;
using AD.ToolsCollection;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace Game.Tutorial
{
    public class TutorialComponent : UIContainer
    {
        [SerializeField] private AssetReference containerRef;

        private ITutorialService tutorialService;

        [Inject]
        public void Inject(ITutorialService tutorialService)
        {
            this.tutorialService = tutorialService;
        }

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(parameters, disp, ct);

            if (tutorialService.ContainerIsShowed(containerRef.AssetGUID))
            {
                return;
            }

            //await tutorialService.ShowContainer(containerRef);
        }
    }
}