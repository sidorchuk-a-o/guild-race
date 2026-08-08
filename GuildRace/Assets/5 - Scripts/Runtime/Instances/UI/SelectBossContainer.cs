using System.Threading;
using AD.Game.Ads;
using AD.Game.Guild;
using AD.Game.Leaderboards;
using AD.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using R3;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace AD.Game.Instances
{
    public class SelectBossContainer : UIContainer
    {
        public const string IDKey = "id";

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
        [SerializeField] private RankRewardContainer rankRewardContainer;
        [SerializeField] private KillCountContainer killCountContainer;
        [Space]
        [SerializeField] private UIText triesCountText;
        [SerializeField] private UIButton startInstanceButton;
        [SerializeField] private AdsButton adsStartButton;

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

            adsStartButton.OnRewarded
                .Subscribe(AddTriesCallback)
                .AddTo(this);

            backButton.OnClick
                .Subscribe(BackClickCallback)
                .AddTo(this);
        }

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(parameters, disp, ct);

            if (parameters.TryGetRouteValue<int>(IDKey, out var instanceId))
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
            unitContainer.SetInteractable(hasUnit);

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

            return;
            async UniTask updateUnit(UnitVM unit, CancellationTokenSource ct)
            {
                if (unit == null)
                {
                    return;
                }

                var image = await unit.LoadImage(ct);

                if (ct.IsCancellationRequested) return;

                unit.SetSelectState(true);

                unitImage.sprite = image;
                unitNameText.SetTextParams(unit.NameKey);
                unitDescText.SetTextParams(unit.DescKey);

                unitAbilities.Init(unit.AbilitiesVM, ct);
                unitRewardContainer.Init(unit.RewardsVM, ct);

                killCountContainer.Init(unit, disp);
                rankRewardContainer.Init(unit, disp);

                unit.ActiveInstanceVM
                    .SilentSubscribe(updateStartButtonState)
                    .AddTo(unitDisp);

                unit.TriesCount
                    .SilentSubscribe(updateStartButtonState)
                    .AddTo(unitDisp);

                unit.CompletedCount
                    .SilentSubscribe(updateStartButtonState)
                    .AddTo(unitDisp);

                unit.CooldownParams.MaxTriesCount
                    .SilentSubscribe(updateStartButtonState)
                    .AddTo(unitDisp);

                adsStartButton.Init(disp);

                adsStartButton.IsCompleted
                    .SilentSubscribe(updateStartButtonState)
                    .AddTo(unitDisp);

                updateStartButtonState();

                return;
                void updateStartButtonState()
                {
                    var hasTries = unit.HasTries;
                    var hasComplete = unit.HasComplete;
                    var hasActiveInstance = unit.HasActiveInstance;

                    var maxTriesCount = unit.CooldownParams.MaxTriesCount.CurrentValue;
                    var triesCount = maxTriesCount - unit.TriesCount.CurrentValue;
                    var adsIsCompleted = adsStartButton.IsCompleted.CurrentValue;

                    triesCountText.SetActive(hasTries && hasComplete && maxTriesCount > 0);
                    triesCountText.SetTextParams(new(triesCountText.LocalizeKey, triesCount, maxTriesCount));

                    adsStartButton.SetActive(!hasActiveInstance && !hasTries && hasComplete && maxTriesCount > 0 && !adsIsCompleted);

                    startInstanceButton.SetActive(!adsStartButton.gameObject.activeSelf);
                    startInstanceButton.SetInteractableState(!hasActiveInstance && hasTries && hasComplete);
                }
            }
        }

        private async void StartSetupInstanceCallback()
        {
            await instancesVMF.StartSetupInstance(new()
            {
                InstanceId = instanceVM.Id,
                BossUnitId = selectedUnitVM.Id
            });
        }

        private void AddTriesCallback()
        {
            instancesVMF.AddTries(selectedUnitVM.Id);
        }

        private void BackClickCallback()
        {
            Router.Push(RouteKeys.Hub.SelectInstance);
        }
    }
}