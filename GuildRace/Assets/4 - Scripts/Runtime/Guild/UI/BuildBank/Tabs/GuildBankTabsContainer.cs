using AD.ToolsCollection;
using Game.Inventory;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Game.Guild
{
    public class GuildBankTabsContainer : MonoBehaviour
    {
        [Header("Tabs")]
        [SerializeField] private List<GuildBankTab> tabs;

        [Header("Selected Tab")]
        [SerializeField] private ItemsGridContainer gridContainer;

        private readonly CompositeDisp gridDisp = new();
        private CompositeDisp disp;

        private GuildBankTabsVM tabsVM;
        private GuildBankTabVM currentTabVM;

        private void Awake()
        {
            foreach (var tab in tabs)
            {
                tab.OnClick
                    .Subscribe(SeletTabCallback)
                    .AddTo(this);
            }
        }

        public void Init(GuildBankTabsVM tabsVM, CompositeDisp disp, bool forcedReset)
        {
            this.tabsVM = tabsVM;
            this.disp = disp;

            currentTabVM = tabsVM[tabs[0].CellType];

            InitTabButtons(disp);
            UpdateCurrentTabView();
        }

        private void InitTabButtons(CompositeDisp disp)
        {
            foreach (var tab in tabs)
            {
                var tabVM = tabsVM[tab.CellType];

                tab.Init(tabVM, disp);
            }
        }

        private void SeletTabCallback(GuildBankTabVM tabVM)
        {
            currentTabVM?.SetSelectState(false);
            currentTabVM = tabVM;

            UpdateCurrentTabView();
        }

        private void UpdateCurrentTabView()
        {
            gridDisp.Clear();
            gridDisp.AddTo(disp);

            currentTabVM.SetSelectState(true);

            gridContainer.Init(currentTabVM.GridVM, gridDisp);
        }
    }
}