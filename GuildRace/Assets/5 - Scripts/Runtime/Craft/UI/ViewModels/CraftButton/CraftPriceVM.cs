using System.Threading;
using AD.Router;
using AD.Store;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace AD.Game.Craft
{
    public class CraftPriceVM : ViewModel
    {
        private readonly RecipeData data;
        private readonly CraftVMFactory craftVMF;

        private readonly CurrencyVM priceVM;
        private readonly ReactiveProperty<CurrencyAmount> price = new();

        public ReadOnlyReactiveProperty<long> Amount { get; }
        public ReadOnlyReactiveProperty<string> AmountStr { get; }
        public ReadOnlyReactiveProperty<bool> IsAvailable { get; }
        public CurrencyAmount Value => price.Value;

        public CraftPriceVM(RecipeData data, CraftVMFactory craftVMF)
        {
            this.data = data;
            this.craftVMF = craftVMF;

            UpdatePrice(0);

            priceVM = craftVMF.StoreVMF.GetCurrency(price);

            Amount = priceVM.Amount;
            AmountStr = priceVM.AmountStr;
            IsAvailable = priceVM.IsAvailable;
        }

        protected override void InitSubscribes()
        {
            priceVM.AddTo(this);
        }

        public void UpdatePrice(int count)
        {
            var amount = data.Price * count;
            var discount = amount * craftVMF.PriceDiscount.CurrentValue;

            price.Value = amount - discount;
        }

        public async UniTask<Sprite> LoadIcon(CancellationTokenSource ct = null)
        {
            return await priceVM.LoadIcon(ct);
        }
    }
}