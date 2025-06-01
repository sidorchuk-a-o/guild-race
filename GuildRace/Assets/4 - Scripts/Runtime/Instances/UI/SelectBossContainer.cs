using System;
using System.Threading;
using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Instances
{
    public class SelectBossContainer : UIContainer
    {
        public const string idKey = "id";

        [Header("Header")]
        [SerializeField] private UIText headerText;
        [SerializeField] private UIButton backButton;

        [Header("Boss Units")]
        [SerializeField] private UnitsScrollView bossUnitsScroll;
        [Space]
        [SerializeField] private CanvasGroup emptyUnitContainer;
        [SerializeField] private CanvasGroup unitContainer;
        [Space]
        [SerializeField] private UIButton startInstanceButton;

        [Header("Empty Unit")]
        [SerializeField] private UIText instanceNameText;

        [Header("Selected Unit")]
        [SerializeField] private UIText unitNameText;

        private readonly CompositeDisp unitDisp = new();
        private CancellationTokenSource unitToken;

        private InstancesVMFactory instancesVMF;
        private InstanceVM instanceVM;

        private UnitVM selectedUnitVM;

        [Inject]
        public void Inject(InstancesVMFactory instancesVMF)
        {
            this.instancesVMF = instancesVMF;
        }

        private void Awake()
        {
            unitContainer.alpha = 0;
            unitContainer.interactable = false;

            startInstanceButton.OnClick
                .Subscribe(StartSetupInstanceCallback)
                .AddTo(this);

            backButton.OnClick
                .Subscribe(BackClickCallback)
                .AddTo(this);
        }

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(parameters, disp, ct);

            if (parameters.TryGetRouteValue<int>(idKey, out var instanceId))
            {
                instanceVM = instancesVMF.GetInstance(instanceId);
                selectedUnitVM = null;
            }

            instanceVM.AddTo(disp);

            // params
            headerText.SetTextParams(instanceVM.NameKey);

            // bosses
            bossUnitsScroll.Init(instanceVM.BossUnitsVM, true);

            bossUnitsScroll.OnSelect
                .Subscribe(x => UnitChangedCallback(x, false))
                .AddTo(disp);

            UnitChangedCallback(selectedUnitVM, force: true);
        }

        private async void UnitChangedCallback(UnitVM unitVM, bool force)
        {
            unitDisp.Clear();
            unitDisp.AddTo(disp);

            selectedUnitVM?.SetSelectState(false);
            selectedUnitVM = unitVM;

            var hasUnit = unitVM != null;
            var token = new CancellationTokenSource();

            unitToken?.Cancel();
            unitToken = token;

            unitContainer.DOKill();
            emptyUnitContainer.DOKill();

            unitContainer.interactable = hasUnit;
            emptyUnitContainer.interactable = !hasUnit;

            const float duration = 0.1f;

            if (force)
            {
                unitContainer.alpha = 0;
                emptyUnitContainer.alpha = 0;
            }
            else
            {
                await UniTask.WhenAll(
                    unitContainer.DOFade(0, duration).ToUniTask(),
                    emptyUnitContainer.DOFade(0, duration).ToUniTask());
            }

            if (token.IsCancellationRequested)
            {
                return;
            }

            InitUnitContainer(unitVM);

            var showContainer = hasUnit
                ? unitContainer
                : emptyUnitContainer;

            if (force)
            {
                showContainer.alpha = 1;
            }
            else
            {
                await showContainer.DOFade(1, duration);
            }
        }

        private void InitUnitContainer(UnitVM unitVM)
        {
            if (unitVM != null)
            {
                unitVM.SetSelectState(true);

                unitNameText.SetTextParams(unitVM.NameKey);

                startInstanceButton.SetInteractableState(!unitVM.HasInstance && !unitVM.WaitResetCooldown);
            }
        }

        private async void StartSetupInstanceCallback()
        {
            await instancesVMF.StartSetupInstance(new SetupInstanceArgs
            {
                InstanceId = instanceVM.Id,
                BossUnitId = selectedUnitVM.Id
            });
        }

        private void BackClickCallback()
        {
            Router.Push(RouteKeys.Hub.selectInstance);
        }
    }
}