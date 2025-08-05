using System.Linq;
using System.Threading;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using AD.Services.Store;
using AD.ToolsCollection;
using UniRx;
using UnityEngine;

namespace Game.Store
{
    public class PurchaseButtonsContainer : MonoBehaviour
    {
        [SerializeField] private List<PurchaseButton> buttons;
        [SerializeField] private GameObject loadingIndicator;

        private PurchaseButton currentButton;

        private void Awake()
        {
            buttons.ForEach(x => x.SetActive(false));
        }

        public async UniTask Init(StoreItemVM itemVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            loadingIndicator.SetActive(true);

            var priceType = itemVM.PriceVM.GetType();
            var button = buttons.FirstOrDefault(x => x.ViewModelType == priceType);

            await button.Init(itemVM, disp, ct);

            if (ct.IsCancellationRequested) return;

            currentButton?.SetActive(false);
            currentButton = button;

            currentButton.SetActive(true);
            loadingIndicator.SetActive(false);
        }
    }
}