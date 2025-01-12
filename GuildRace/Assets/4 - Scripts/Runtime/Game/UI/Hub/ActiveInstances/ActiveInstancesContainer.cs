using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Instances
{
    public class ActiveInstancesContainer : UIContainer
    {
        [Header("Instances")]
        [SerializeField] private ActiveInstancesScrollView activeInstancesScroll;

        [Header("Instance")]
        [SerializeField] private CanvasGroup instanceContainer;
        [SerializeField] private UIButton completeInstanceButton;
        [Space]
        [SerializeField] private UIText instanceNameText;

        private readonly CompositeDisp instanceDisp = new();
        private CancellationTokenSource instanceToken;

        private InstancesVMFactory instancesVMF;

        private ActiveInstancesVM activeInstancesVM;

        private ActiveInstanceVM activeInstanceVM;
        private ActiveInstanceVM switchToInstanceVM;

        [Inject]
        public void Inject(InstancesVMFactory instancesVMF)
        {
            this.instancesVMF = instancesVMF;

            activeInstancesVM = instancesVMF.GetActiveInstances();
        }

        private void Awake()
        {
            instanceContainer.alpha = 0;
            instanceContainer.interactable = false;

            completeInstanceButton.OnClick
                .Subscribe(CompleteInstanceCallback)
                .AddTo(this);
        }

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp)
        {
            await base.Init(parameters, disp);

            var hasBack = parameters.HasBackRouteKey();
            var hasForcedReset = parameters.HasForceReset();

            activeInstancesVM.AddTo(disp);
            activeInstancesScroll.Init(activeInstancesVM, true);

            activeInstancesScroll.OnSelect
                .Subscribe(InstanceSelectCallback)
                .AddTo(disp);

            if (hasBack)
            {
                if (switchToInstanceVM != null)
                {
                    SelectActiveInstance(switchToInstanceVM);

                    switchToInstanceVM = null;
                }

                if (activeInstanceVM == null)
                {
                    instanceContainer.alpha = 0;
                    instanceContainer.interactable = false;
                }

                return;
            }

            SelectActiveInstance(activeInstancesVM.FirstOrDefault());
        }

        private void InstanceSelectCallback(ActiveInstanceVM activeInstanceVM)
        {
            SelectActiveInstance(activeInstanceVM);
        }

        private void SelectActiveInstance(ActiveInstanceVM activeInstanceVM)
        {
            if (this.activeInstanceVM == activeInstanceVM)
            {
                return;
            }

            this.activeInstanceVM?.SetSelectState(false);

            this.activeInstanceVM = activeInstanceVM;
            this.activeInstanceVM?.SetSelectState(true);

            UpdateActiveInstanceBlock();
        }

        private async void UpdateActiveInstanceBlock()
        {
            instanceDisp.Clear();
            instanceDisp.AddTo(disp);

            var selected = activeInstanceVM != null;
            var token = new CancellationTokenSource();

            instanceToken?.Cancel();
            instanceToken = token;

            instanceContainer.DOKill();
            instanceContainer.interactable = selected;

            const float duration = 0.1f;

            await instanceContainer.DOFade(0, duration);

            if (selected == false || token.IsCancellationRequested)
            {
                return;
            }

            instanceNameText.SetTextParams(activeInstanceVM.InstanceVM.NameKey);

            activeInstanceVM.IsReadyToComplete
                .Subscribe(x => completeInstanceButton.SetInteractableState(x))
                .AddTo(instanceDisp);

            await instanceContainer.DOFade(1, duration);
        }

        private void CompleteInstanceCallback()
        {
            if (activeInstanceVM == null)
            {
                return;
            }

            var index = instancesVMF.StopActiveInstance(activeInstanceVM.Id);
            var switchToInstanceVM = activeInstancesVM.NearbyOrDefault(index);

            activeInstanceVM = null;

            SelectActiveInstance(switchToInstanceVM);

            if (activeInstanceVM == null)
            {
                instanceContainer.alpha = 0;
                instanceContainer.interactable = false;
            }
        }
    }
}