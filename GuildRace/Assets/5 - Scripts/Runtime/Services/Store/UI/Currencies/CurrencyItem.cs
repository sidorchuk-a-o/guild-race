using System.Threading;
using AD.Game.UI;
using AD.Store;
using AD.ToolsCollection;
using AD.UI;
using R3;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace AD.Game.Store
{
    public class CurrencyItem : MonoBehaviour
    {
        [SerializeField] private CurrencyKey currencyKey;
        [Space]
        [SerializeField] private Image iconImage;
        [SerializeField] private UIText amountText;
        [SerializeField] private CurrencyChangedContainer changedContainer;
        [SerializeField] private TooltipComponent tooltipComponent;

        private CurrencyVM currencyVM;

        [Inject]
        public void Inject(StoreVMFactory storeVMF)
        {
            currencyVM = storeVMF.GetCurrency(currencyKey);
        }

        public async void Init(CompositeDisp disp, CancellationTokenSource ct)
        {
            currencyVM.AddTo(disp);

            var icon = await currencyVM.LoadIcon(ct);

            if (ct.IsCancellationRequested) return;

            iconImage.sprite = icon;
            changedContainer.Init(currencyVM, disp);
            tooltipComponent.Init(currencyVM);

            currencyVM.AmountStr
                .Subscribe(x => amountText.SetTextParams(x))
                .AddTo(disp);
        }
    }
}