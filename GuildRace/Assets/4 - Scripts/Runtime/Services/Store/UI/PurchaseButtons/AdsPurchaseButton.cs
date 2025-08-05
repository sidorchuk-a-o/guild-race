using System;
using AD.Services.Store;

namespace Game.Store
{
    public class AdsPurchaseButton : PurchaseButton
    {
        public override Type ViewModelType { get; } = typeof(AdsPriceVM);
    }
}