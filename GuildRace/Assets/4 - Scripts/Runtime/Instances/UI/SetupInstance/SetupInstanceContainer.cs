using AD.Services.Localization;
using AD.Services.Router;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using Game.Guild;
using Game.Inventory;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace Game.Instances
{
    public class SetupInstanceContainer : UIContainer
    {
        [Header("Header")]
        [SerializeField] private UIText headerText;
        [SerializeField] private LocalizeKey headerKey;

        [Header("Squad")]
        [SerializeField] private CharactersTabsContainer charactersContainer;
        [SerializeField] private SquadRolesCountersContainer rolesCountersContainer;
        [SerializeField] private Transform squadContainerRoot;
        [SerializeField] private List<SquadContainerParams> squadContainers;

        [Header("Bag")]
        [SerializeField] private GuildBankTabsContainer guildBankContainer;
        [SerializeField] private ItemsGridContainer squadBagContainer;

        [Header("Params")]
        [SerializeField] private Toggle playerInstanceToggle;

        [Header("Button")]
        [SerializeField] private UIButton backButton;
        [SerializeField] private UIButton startButton;

        private InstancesVMFactory instancesVMF;
        private IObjectResolver resolver;

        private ActiveInstanceVM activeInstanceVM;

        private GuildBankTabsVM bankTabsVM;
        private IReadOnlyList<RoleTabVM> roleTabsVM;

        private SquadUnitsContainer squadUnitsContainer;

        [Inject]
        public void Inject(InstancesVMFactory instancesVMF, GuildVMFactory guildVMF, IObjectResolver resolver)
        {
            this.instancesVMF = instancesVMF;
            this.resolver = resolver;

            bankTabsVM = guildVMF.GetGuildBankTabs();
        }

        private void Awake()
        {
            backButton.OnClick
                .Subscribe(BackCallback)
                .AddTo(this);

            startButton.OnClick
                .Subscribe(StartCallback)
                .AddTo(this);
        }

        protected override async UniTask Init(RouteParams parameters, CompositeDisp disp)
        {
            await base.Init(parameters, disp);

            var hasForcedReset = parameters.HasForceReset();

            activeInstanceVM = instancesVMF.GetSetupInstance();
            activeInstanceVM.AddTo(disp);

            // upd params

            var headerKey = activeInstanceVM.InstanceVM.NameKey;
            var headerData = new UITextData(this.headerKey, headerKey);

            headerText.SetTextParams(headerData);

            // characters

            roleTabsVM = instancesVMF.GetCharactersByRoles();
            roleTabsVM.ForEach(tab => tab.AddTo(disp));

            charactersContainer.Init(roleTabsVM, activeInstanceVM, disp);
            rolesCountersContainer.Init(activeInstanceVM, disp);

            SpawnSquadContainer(disp);

            // guild bank

            bankTabsVM.AddTo(disp);

            guildBankContainer.Init(bankTabsVM, disp, hasForcedReset);
            squadBagContainer.Init(activeInstanceVM.BagVM, disp);
        }

        private void SpawnSquadContainer(CompositeDisp disp)
        {
            var instanceType = activeInstanceVM.InstanceVM.Type;
            var containerParams = squadContainers.FirstOrDefault(x => x.Type == instanceType);

            var prefab = containerParams.SquadPrefab;

            squadUnitsContainer = Instantiate(prefab, squadContainerRoot);
            squadUnitsContainer.transform.SetAsFirstSibling();

            resolver.InjectGameObject(squadUnitsContainer.gameObject);

            squadUnitsContainer.Init(activeInstanceVM, disp);
        }

        private void BackCallback()
        {
            Router.Back(LoadingScreenKeys.loading);
        }

        private async void StartCallback()
        {
            SetButtonsState(false);

            var playerInstance = playerInstanceToggle.isOn;

            await instancesVMF.CompleteSetupAndStartInstance(playerInstance);

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

            Destroy(squadUnitsContainer.gameObject);
        }
    }
}