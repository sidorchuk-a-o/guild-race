using AD.Services.Router;
using AD.ToolsCollection;
using Game.Guild;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Instances
{
    public class CharactersTabsContainer : MonoBehaviour
    {
        [Header("Tabs")]
        [SerializeField] private List<RoleTab> tabs;

        [Header("Selected Tab")]
        [SerializeField] private CharactersByRoleScrollView charactersScroll;

        private IReadOnlyList<RoleTabVM> tabsVM;

        private ActiveInstanceVM activeInstanceVM;
        private RoleTabVM currentTabVM;

        private InstancesVMFactory instancesVMF;

        [Inject]
        public void Inject(InstancesVMFactory instancesVMF)
        {
            this.instancesVMF = instancesVMF;
        }

        private void Awake()
        {
            foreach (var tab in tabs)
            {
                tab.OnClick
                    .Subscribe(SelectTabCallback)
                    .AddTo(this);
            }
        }

        public void Init(IReadOnlyList<RoleTabVM> tabsVM, ActiveInstanceVM activeInstanceVM, CompositeDisp disp)
        {
            this.tabsVM = tabsVM;
            this.activeInstanceVM = activeInstanceVM;

            InitTabButtons(disp);

            SelectTabCallback(null);

            charactersScroll.OnSelect
                .Subscribe(SelectCharacterCallback)
                .AddTo(disp);
        }

        private void InitTabButtons(CompositeDisp disp)
        {
            foreach (var tabVM in tabsVM)
            {
                var tab = tabs.First(x => x.Role == tabVM.Role);

                tab.Init(tabVM, disp);
            }
        }

        private void SelectTabCallback(RoleTabVM tabVM)
        {
            currentTabVM?.SetSelectState(false);
            currentTabVM = tabVM;

            UpdateCurrentTabView();
        }

        private void UpdateCurrentTabView()
        {
            currentTabVM ??= tabsVM.FirstOrDefault(x => x.Role == tabs[0].Role);
            currentTabVM.SetSelectState(true);

            charactersScroll.SetInstance(activeInstanceVM);
            charactersScroll.Init(currentTabVM.CharactersVM);
        }

        private void SelectCharacterCallback(CharacterVM characterVM)
        {
            if (characterVM.HasInstance)
            {
                instancesVMF.TryRemoveCharacterFromSquad(characterVM.Id);
            }
            else
            {
                instancesVMF.TryAddCharacterToSquad(characterVM.Id);
            }
        }
    }
}