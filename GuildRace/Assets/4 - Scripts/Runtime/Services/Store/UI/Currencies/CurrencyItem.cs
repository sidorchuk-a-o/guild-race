using System.Threading;
using AD.Services.Store;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.Store
{
    public class CurrencyItem : MonoBehaviour
    {
        [SerializeField] private CurrencyKey currencyKey;
        [Space]
        [SerializeField] private Image iconImage;
        [SerializeField] private UIText amountText;
        [SerializeField] private CurrencyChangedContainer changedContainer;

        private CurrencyVM currencyVM;

        [Inject]
        public void Inject(StoreVMFactory storeVMF)
        {
            currencyVM = storeVMF.GetCurrency(currencyKey);
        }

        public async void Init(CompositeDisp disp, CancellationTokenSource ct)
        {
            currencyVM.AddTo(disp);

            var icon = await currencyVM.LoadIcon(ct);

            if (ct.IsCancellationRequested) return;

            iconImage.sprite = icon;
            changedContainer.Init(currencyVM, disp);

            currencyVM.AmountStr
                .Subscribe(x => amountText.SetTextParams(x))
                .AddTo(disp);
        }
    }
}