using System;
using System.Threading;
using AD.Store;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AD.Game.Store
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

            var inAppPriceVM = (InAppPriceVM)priceVM;
            var icon = await inAppPriceVM.ProductVM.LoadIcon(ct);

            if (ct.IsCancellationRequested) return;

            iconImage.SetImage(icon);
            priceText.SetTextParams(inAppPriceVM.ProductVM.LocalizedPrice);
        }
    }
}