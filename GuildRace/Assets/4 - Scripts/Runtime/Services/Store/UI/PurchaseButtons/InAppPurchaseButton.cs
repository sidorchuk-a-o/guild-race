using System;
using System.Threading;
using AD.Services.Store;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Store
{
    public class InAppPurchaseButton : PurchaseButton
    {
        [Header("In App")]
        [SerializeField] private Image iconImage;
        [SerializeField] private UIText priceText;

        public override Type ViewModelType { get; } = typeof(InAppPriceVM);

        protected override async UniTask InitPrice(PriceVM priceVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.InitPrice(priceVM, disp, ct);

            var inAppPriceVM = priceVM as InAppPriceVM;
            var icon = await inAppPriceVM.ProductVM.LoadIcon(ct);

            if (ct.IsCancellationRequested) return;

            iconImage.sprite = icon;
            priceText.SetTextParams(inAppPriceVM.ProductVM.LocalizedPrice);
        }
    }
}