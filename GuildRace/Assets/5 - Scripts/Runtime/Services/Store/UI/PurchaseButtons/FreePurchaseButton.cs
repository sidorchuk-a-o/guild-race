using System;
using AD.Store;

namespace AD.Game.Store
{
    public class FreePurchaseButton : PurchaseButton
    {
        public override Type ViewModelType { get; } = typeof(FreePriceVM);
    }
}