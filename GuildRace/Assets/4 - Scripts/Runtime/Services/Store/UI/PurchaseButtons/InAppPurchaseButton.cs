using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using AD.UI;
using AD.Services.Store;
using AD.ToolsCollection;
using UnityEngine;

namespace Game.Store
{
    public class InAppPurchaseButton : PurchaseButton
    {
        [Header("In App")]
        [SerializeField] private ImageContainer iconImage;
        [SerializeField] private UIText priceText;

        public override Type ViewModelType { get; } = typeof(InAppPriceVM);

        protected override async UniTask InitPrice(PriceVM priceVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.InitPrice(priceVM, disp, ct);

            var inAppPriceVM = priceVM as InAppPriceVM;
            var icon = await inAppPriceVM.ProductVM.LoadIcon(ct);

            if (ct.IsCancellationRequested) return;

            iconImage.SetImage(icon);
            priceText.SetTextParams(inAppPriceVM.ProductVM.LocalizedPrice);
        }
    }
}