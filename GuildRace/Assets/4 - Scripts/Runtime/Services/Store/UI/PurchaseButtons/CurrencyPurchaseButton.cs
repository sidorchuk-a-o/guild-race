using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using AD.UI;
using AD.Services.Store;
using AD.ToolsCollection;
using UniRx;
using UnityEngine;

namespace Game.Store
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

            var currencyPriceVM = priceVM as CurrencyPriceVM;
            var currencyVM = currencyPriceVM.CurrencyVM;

            var icon = await currencyVM.LoadIcon(ct);

            if (ct.IsCancellationRequested) return;

            iconImage.SetImage(icon);
            priceText.SetTextParams(currencyVM.Amount.Value);

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