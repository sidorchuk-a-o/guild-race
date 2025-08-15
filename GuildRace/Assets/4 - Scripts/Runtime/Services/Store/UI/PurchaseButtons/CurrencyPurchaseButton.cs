using System;
using System.Threading;
using AD.Services.Store;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Store
{
    public class CurrencyPurchaseButton : PurchaseButton
    {
        [Header("Currency")]
        [SerializeField] private Image iconImage;
        [SerializeField] private UIText priceText;
        [Space]
        [SerializeField] private UIButton priceButton;
        [SerializeField] private string defaultStateKey = "default";
        [SerializeField] private string errorStateKey = "error";

        public override Type ViewModelType { get; } = typeof(CurrencyPriceVM);

        protected override async UniTask InitPrice(PriceVM priceVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.InitPrice(priceVM, disp, ct);

            var currencyPriceVM = priceVM as CurrencyPriceVM;
            var currencyVM = currencyPriceVM.CurrencyVM;

            var icon = await currencyVM.LoadIcon(ct);

            if (ct.IsCancellationRequested) return;

            iconImage.sprite = icon;
            priceText.SetTextParams(currencyVM.Amount.Value);

            var storeCurrencyVM = StoreVMF.GetCurrency(currencyVM.Key);
            storeCurrencyVM.AddTo(disp);

            storeCurrencyVM.Amount
                .Subscribe(x => UpdatePriceState(storeCurrencyVM, currencyVM))
                .AddTo(disp);
        }

        private void UpdatePriceState(CurrencyVM storeCurrencyVM, CurrencyVM priceCurrencyVM)
        {
            var available = storeCurrencyVM.Amount.Value >= priceCurrencyVM.Amount.Value;
            var stateKey = available ? defaultStateKey : errorStateKey;

            priceButton.SetState(stateKey);
            priceButton.SetInteractableState(available);
        }
    }
}