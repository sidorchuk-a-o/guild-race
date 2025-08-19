using System.Threading;
using AD.Services.Router;
using AD.Services.Store;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Game.Craft
{
    public class CraftPriceVM : ViewModel
    {
        private readonly RecipeData data;
        private readonly CraftVMFactory craftVMF;

        private readonly CurrencyVM priceVM;
        private readonly ReactiveProperty<CurrencyAmount> price = new();

        public IReadOnlyReactiveProperty<long> Amount { get; }
        public IReadOnlyReactiveProperty<string> AmountStr { get; }
        public IReadOnlyReactiveProperty<bool> IsAvailable { get; }
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
            var price = data.Price * count;
            var discount = price * craftVMF.PriceDiscount.Value;

            this.price.Value = price - discount;
        }

        public async UniTask<Sprite> LoadIcon(CancellationTokenSource ct = null)
        {
            return await priceVM.LoadIcon(ct);
        }
    }
}