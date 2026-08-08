using System;
using System.Threading;
using AD.Store;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace AD.Game.Store
{
    public class CurrencyPurchaseButton : PurchaseButton
    {
        [Header("Currency")]
        [SerializeField] private ImageContainer iconImage;
        [SerializeField] private UIText priceText;
        [Space]
        [SerializeField] private UIButton priceButton;
        [SerializeField] private string defaultStateKey = "default";
        [SerializeField] private string errorStateKey = "error";

        public override Type ViewModelType { get; } = typeof(CurrencyPriceVM);

        protected override async UniTask InitPrice(PriceVM priceVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.InitPrice(priceVM, disp, ct);

            var currencyPriceVM = (CurrencyPriceVM)priceVM;
            var currencyVM = currencyPriceVM.CurrencyVM;

            var icon = await currencyVM.LoadIcon(ct);

            if (ct.IsCancellationRequested) return;

            iconImage.SetImage(icon);
            priceText.SetTextParams(currencyVM.Amount.CurrentValue);

            currencyPriceVM.CurrencyVM.IsAvailable
                .Subscribe(UpdatePriceState)
                .AddTo(disp);
        }

        private void UpdatePriceState(bool isAvailable)
        {
            var stateKey = isAvailable ? defaultStateKey : errorStateKey;

            priceButton.SetState(stateKey);
            priceButton.SetInteractableState(isAvailable);
        }
    }
}