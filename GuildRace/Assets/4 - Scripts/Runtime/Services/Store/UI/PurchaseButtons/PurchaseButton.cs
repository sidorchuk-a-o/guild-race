using System;
using System.Threading;
using AD.Services.Store;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using VContainer;

namespace Game.Store
{
    public abstract class PurchaseButton : MonoBehaviour
    {
        [SerializeField] private UIButton button;
        [SerializeField] private GameObject loadingIndicator;

        private IStoreService storeService;
        private StoreVMFactory storeVMF;

        private StoreItemVM itemVM;
        private bool inProcess = true;

        public abstract Type ViewModelType { get; }
        protected StoreVMFactory StoreVMF => storeVMF;

        [Inject]
        public void Inject(IStoreService storeService, StoreVMFactory storeVMF)
        {
            this.storeService = storeService;
            this.storeVMF = storeVMF;
        }

        private void Awake()
        {
            button.OnClick
                .Subscribe(TryPurchaseCallback)
                .AddTo(this);
        }

        public virtual async UniTask Init(StoreItemVM itemVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            this.itemVM = itemVM;

            ShowLoading();

            await InitPrice(itemVM.PriceVM, disp, ct);

            HideLoading();
        }

        protected virtual UniTask InitPrice(PriceVM priceVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            return UniTask.CompletedTask;
        }

        private async void TryPurchaseCallback()
        {
            if (inProcess)
            {
                return;
            }

            ShowLoading();

            var result = await storeService.ProcessPurchaseItem(itemVM.Id);

            if (result.Success)
            {
                this.LogMsg($"Purchase Result( {itemVM.Id}, {result.Success} )");

                switch (result.Value)
                {
                    case PurchaseResult.Success: PurchaseSuccess(); break;
                    case PurchaseResult.Cancel: PurchaseCancel(); break;
                }
            }
            else
            {
                this.LogMsg($"Purchase Fail( {itemVM.Id}, {result.Error} )");

                PurchaseFail();
            }

            HideLoading();
        }

        protected virtual void PurchaseSuccess()
        {
        }

        protected virtual void PurchaseCancel()
        {
        }

        protected virtual void PurchaseFail()
        {
        }

        private void ShowLoading()
        {
            loadingIndicator.SetActive(true);
            inProcess = true;
        }

        private void HideLoading()
        {
            loadingIndicator.SetActive(false);
            inProcess = false;
        }
    }
}