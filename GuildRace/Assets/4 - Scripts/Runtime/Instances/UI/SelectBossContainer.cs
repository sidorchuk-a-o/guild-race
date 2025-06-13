using System.Threading;
using Cysharp.Threading.Tasks;
using AD.UI;
using DG.Tweening;
using AD.Services.Router;
using AD.ToolsCollection;
using UniRx;
using UnityEngine;
using VContainer;
using UnityEngine.UI;
using Game.Guild;

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
        [SerializeField] private CanvasGroup unitContainer;
        [Space]
        [SerializeField] private UIText unitNameText;
        [SerializeField] private UIText unitDescText;
        [SerializeField] private Image unitImage;
        [SerializeField] private AbilitiesContainer unitAbilities;
        [SerializeField] private RewardsContainer unitRewardContainer;
        [Space]
        [SerializeField] private UIButton startInstanceButton;

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

            // select unit
            var targetUnit = selectedUnitVM ?? instanceVM.BossUnitsVM.FirstOrDefault();

            UnitChangedCallback(targetUnit, force: true);
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
            unitContainer.interactable = hasUnit;

            const float duration = 0.1f;

            if (force)
            {
                unitContainer.alpha = 0;
            }
            else
            {
                await unitContainer.DOFade(0, duration);
            }

            if (token.IsCancellationRequested)
            {
                return;
            }

            await updateUnit(unitVM, token);

            if (force)
            {
                unitContainer.alpha = 1;
            }
            else
            {
                await unitContainer.DOFade(1, duration);
            }

            async UniTask updateUnit(UnitVM unitVM, CancellationTokenSource ct)
            {
                if (unitVM == null)
                {
                    return;
                }

                var image = await unitVM.LoadImage(ct);

                if (ct.IsCancellationRequested) return;

                unitVM.SetSelectState(true);

                unitImage.sprite = image;
                unitNameText.SetTextParams(unitVM.NameKey);
                unitDescText.SetTextParams(unitVM.DescKey);

                unitAbilities.Init(unitVM.AbilitiesVM, ct);
                unitRewardContainer.Init(unitVM.RewardsVM, ct);

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