using AD.ToolsCollection;
using AD.UI;
using Game.Guild;
using System;
using UniRx;
using UnityEngine;

namespace Game.Instances
{
    public class RoleTab : MonoBehaviour
    {
        [SerializeField] private RoleId role;

        [Header("Tab")]
        [SerializeField] private UIText nameText;

        [Header("Button")]
        [SerializeField] private UIButton button;
        [SerializeField] private string selectedStateKey = "selected";
        [SerializeField] private string unselectedStateKey = "default";

        private readonly Subject<RoleTabVM> onClick = new();

        private RoleTabVM tabVM;

        public RoleId Role => role;
        public IObservable<RoleTabVM> OnClick => onClick;

        private void Awake()
        {
            button.OnClick
                .Subscribe(ClickCallback)
                .AddTo(this);
        }

        public void Init(RoleTabVM tabVM, CompositeDisp disp)
        {
            this.tabVM = tabVM;

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