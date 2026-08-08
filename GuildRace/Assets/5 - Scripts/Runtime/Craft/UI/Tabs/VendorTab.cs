using AD.ToolsCollection;
using AD.UI;
using R3;
using UnityEngine;

namespace AD.Game.Craft
{
    public class VendorTab : MonoBehaviour
    {
        [Header("Tab")]
        [SerializeField] private UIText nameText;

        [Header("Button")]
        [SerializeField] private UIButton button;
        [SerializeField] private string selectedStateKey = "selected";
        [SerializeField] private string unselectedStateKey = "default";

        private readonly ReactiveEvent<VendorVM> onClick = new();

        private VendorVM vendorVM;

        public ReadOnlyReactiveEvent<VendorVM> OnClick => onClick;

        private void Awake()
        {
            button.OnClick
                .Subscribe(ClickCallback)
                .AddTo(this);
        }

        public void Init(VendorVM vendorVM, CompositeDisp disp)
        {
            this.vendorVM = vendorVM;

            nameText.SetTextParams(vendorVM.NameKey);

            vendorVM.IsSelected
                .Subscribe(SelectedStateChanged)
                .AddTo(disp);
        }

        private void ClickCallback()
        {
            onClick.OnNext(vendorVM);
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