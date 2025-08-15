using System;
using AD.Services.Store;

namespace Game.Store
{
    public class FreePurchaseButton : PurchaseButton
    {
        public override Type ViewModelType { get; } = typeof(FreePriceVM);
    }
}