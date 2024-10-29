using AD.ToolsCollection;
using AD.UI;
using Game.Inventory;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Guild
{
    public class GuildBankTab : MonoBehaviour
    {
        [SerializeField] private ItemsGridCellType cellType;

        [Header("Tab")]
        [SerializeField] private Image iconImage;
        [SerializeField] private UIText nameText;

        [Header("Button")]
        [SerializeField] private UIButton button;
        [SerializeField] private string selectedStateKey = "selected";
        [SerializeField] private string unselectedStateKey = "default";

        private readonly Subject<GuildBankTabVM> onClick = new();

        private GuildBankTabVM tabVM;

        public ItemsGridCellType CellType => cellType;
        public IObservable<GuildBankTabVM> OnClick => onClick;

        private void Awake()
        {
            button.OnClick
                .Subscribe(ClickCallback)
                .AddTo(this);
        }

        public async void Init(GuildBankTabVM tabVM, CompositeDisp disp)
        {
            this.tabVM = tabVM;

            if (iconImage)
            {
                iconImage.sprite = await tabVM.LoadIcon();
            }

            if (nameText)
            {
                nameText.SetTextParams(tabVM.NameKey);
            }

            tabVM.IsSelected
                .Subscribe(SelectedStateChanged)
                .AddTo(disp);
        }

        private void ClickCallback()
        {
            onClick.OnNext(tabVM);
        }

        private void SelectedStateChanged(bool state)
        {
            var stateKey = state
                ? selectedStateKey
                : unselectedStateKey;

            button.SetState(stateKey);
        }
    }
}