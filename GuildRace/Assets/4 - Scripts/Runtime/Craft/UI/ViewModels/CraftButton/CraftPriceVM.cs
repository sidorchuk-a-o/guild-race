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
        private readonly CurrencyVM priceVM;
        private readonly CurrencyVM currencyVM;

        private readonly ReactiveProperty<CurrencyAmount> price = new();
        private readonly ReactiveProperty<bool> isAvailable = new();

        public IReadOnlyReactiveProperty<long> Amount { get; }
        public IReadOnlyReactiveProperty<string> AmountStr { get; }
        public IReadOnlyReactiveProperty<bool> IsAvailable => isAvailable;
        public CurrencyAmount Value => price.Value;

        public CraftPriceVM(RecipeData data, CraftVMFactory craftVMF)
        {
            this.data = data;

            price.Value = data.Price;
            priceVM = craftVMF.StoreVMF.GetCurrency(price);
            currencyVM = craftVMF.StoreVMF.GetCurrency(data.Price.Key);

            Amount = priceVM.Amount;
            AmountStr = priceVM.AmountStr;
        }

        protected override void InitSubscribes()
        {
            priceVM.AddTo(this);
            currencyVM.AddTo(this);
        }

        public void UpdatePrice(int count)
        {
            price.Value = data.Price * count;
            isAvailable.Value = currencyVM.Amount.Value >= priceVM.Amount.Value;
        }

        public async UniTask<Sprite> LoadIcon(CancellationTokenSource ct = null)
        {
            return await priceVM.LoadIcon(ct);
        }
    }
}