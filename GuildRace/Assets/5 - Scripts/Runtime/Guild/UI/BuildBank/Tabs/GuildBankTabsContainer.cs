using System.Collections.Generic;
using AD.Game.Inventory;
using AD.ToolsCollection;
using R3;
using UnityEngine;

namespace AD.Game.Guild
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
        private ItemsGridCellType currentCellType;

        private void Awake()
        {
            foreach (var tab in tabs)
            {
                tab.OnClick
                    .Subscribe(x => SelectTabCallback(x, tab))
                    .AddTo(this);
            }
        }

        public void Init(GuildBankTabsVM tabsVM, CompositeDisp disp)
        {
            this.tabsVM = tabsVM;
            this.disp = disp;

            currentCellType ??= tabs[0].CellType;
            currentTabVM = tabsVM[currentCellType];

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

        private void SelectTabCallback(GuildBankTabVM tabVM, GuildBankTab tab)
        {
            currentTabVM?.SetSelectState(false);

            currentCellType = tab.CellType;
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