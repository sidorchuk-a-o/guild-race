using System;
using System.Threading;
using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Ads;
using Game.Guild;
using Game.Leaderboards;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
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

                killCountContainer.Init(unitVM, disp);
                rankRewardContainer.Init(unitVM, disp);

                unitVM.ActiveInstanceVM
                    .SilentSubscribe(updateStartButtonState)
                    .AddTo(unitDisp);

                unitVM.TriesCount
                    .SilentSubscribe(updateStartButtonState)
                    .AddTo(unitDisp);

                unitVM.CompletedCount
                    .SilentSubscribe(updateStartButtonState)
                    .AddTo(unitDisp);

                adsStartButton.Init(disp);

                adsStartButton.IsCompleted
                    .SilentSubscribe(updateStartButtonState)
                    .AddTo(unitDisp);

                updateStartButtonState();

                void updateStartButtonState()
                {
                    var hasTries = unitVM.HasTries;
                    var hasComplete = unitVM.HasComplete;
                    var hasActiveInstance = unitVM.HasActiveInstance;

                    var maxTriesCount = unitVM.CooldownParams.MaxTriesCount;
                    var triesCount = maxTriesCount - unitVM.TriesCount.Value;
                    var adsIsCompleted = adsStartButton.IsCompleted.Value;

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
            await instancesVMF.StartSetupInstance(new SetupInstanceArgs
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