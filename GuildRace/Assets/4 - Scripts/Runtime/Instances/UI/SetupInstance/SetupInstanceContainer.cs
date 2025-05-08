using System.Threading;
using AD.Services.Localization;
using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using Game.Guild;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Instances
{
    public class SetupInstanceContainer : UIContainer
    {
        [Header("Header")]
        [SerializeField] private UIText headerText;
        [SerializeField] private LocalizeKey headerKey;

        [Header("Tabs")]
        [SerializeField] private ContentTab[] contentTabs;
        [SerializeField] private SquadCandidatesScrollView squadCandidatesScroll;
        [SerializeField] private GuildBankTabsContainer guildBankContainer;

        [Header("Squad")]
        [SerializeField] private SquadUnitsContainer squadUnitsContainer;

        [Header("Boss Unit")]
        [SerializeField] private UIText bossNameText;
        [SerializeField] private ThreatsContainer bossThreatsContainer;
        [Space]
        [SerializeField] private UIText completeChanceText;

        [Header("Button")]
        [SerializeField] private UIButton backButton;
        [SerializeField] private UIButton startButton;

        private GuildVMFactory guildVMF;
        private InstancesVMFactory instancesVMF;

        private ContentTab currentTab;

        private ActiveInstanceVM setupInstanceVM;
        private SquadCandidatesVM squadCandidatesVM;
        private GuildBankTabsVM bankTabsVM;

        [Inject]
        public void Inject(GuildVMFactory guildVMF, InstancesVMFactory instancesVMF)
        {
            this.guildVMF = guildVMF;
            this.instancesVMF = instancesVMF;
        }

        private void Awake()
        {
            backButton.OnClick
                .Subscribe(BackCallback)
                .AddTo(this);

            startButton.OnClick
                .Subscribe(StartCallback)
                .AddTo(this);

            for (var i = 0; i < contentTabs.Length; i++)
            {
                var contentTab = contentTabs[i];

                contentTab.OnClick
                    .Subscribe(SelectTabCallback)
                    .AddTo(this);

                var firstTab = i == 0;

                if (firstTab)
                {
                    currentTab = contentTab;
                }

                contentTab.SetContentState(firstTab);
            }
        }

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(parameters, disp, ct);

            var hasForcedReset = parameters.HasForceReset();

            setupInstanceVM = instancesVMF.GetSetupInstance();
            setupInstanceVM.AddTo(disp);

            squadCandidatesVM = instancesVMF.GetSquadCandidates();
            squadCandidatesVM.AddTo(disp);

            bankTabsVM = guildVMF.GetGuildBankTabs();
            bankTabsVM.AddTo(disp);

            // upd params
            var headerKey = setupInstanceVM.InstanceVM.NameKey;
            var headerData = new UITextData(this.headerKey, headerKey);

            headerText.SetTextParams(headerData);

            // characters
            squadCandidatesScroll.Init(squadCandidatesVM, true);

            squadCandidatesScroll.OnSelect
                .Subscribe(SelectCharacterCallback)
                .AddTo(disp);

            // guild bank
            guildBankContainer.Init(bankTabsVM, disp, hasForcedReset);

            // squad
            squadUnitsContainer.Init(setupInstanceVM, disp);

            // boss
            bossNameText.SetTextParams(setupInstanceVM.BossUnitVM.NameKey);

            setupInstanceVM.CompleteChance
                .Subscribe(x => completeChanceText.SetTextParams(x))
                .AddTo(disp);

            await bossThreatsContainer.Init(setupInstanceVM.ThreatsVM, disp, ct);
        }

        private void SelectTabCallback(ContentTab contentTab)
        {
            if (currentTab == contentTab)
            {
                return;
            }

            currentTab.SetContentState(false);
            currentTab = contentTab;
            currentTab.SetContentState(true);
        }

        private void SelectCharacterCallback(SquadCandidateVM squadCandidateVM)
        {
            if (squadCandidateVM.CharacterVM.HasInstance)
            {
                instancesVMF.TryRemoveCharacterFromSquad(squadCandidateVM.CharacterVM.Id);
            }
            else
            {
                instancesVMF.TryAddCharacterToSquad(squadCandidateVM.CharacterVM.Id);
            }
        }

        private void BackCallback()
        {
            Router.Push(RouteKeys.Hub.selectInstance, LoadingScreenKeys.loading);
        }

        private async void StartCallback()
        {
            SetButtonsState(false);

            await instancesVMF.CompleteSetupAndStartInstance();

            SetButtonsState(true);
        }

        private void SetButtonsState(bool state)
        {
            backButton.SetInteractableState(state);
            startButton.SetInteractableState(state);
        }

        public override void DisableElement()
        {
            base.DisableElement();

            instancesVMF.CancelSetupInstance();
        }
    }
}