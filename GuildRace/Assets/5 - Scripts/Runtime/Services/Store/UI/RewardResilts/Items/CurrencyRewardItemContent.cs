using System.Threading;
using AD.Store;
using AD.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace AD.Game.Store
{
    public class CurrencyRewardItemContent : RewardItemContent
    {
        [Header("Currency")]
        [SerializeField] private Image iconImage;
        [SerializeField] private UIText amountText;

        public override async UniTask Init(RewardResultVM rewardVM, CancellationTokenSource ct)
        {
            var currencyRewardVM = (CurrencyRewardResultVM)rewardVM;
            var currencyVM = currencyRewardVM.CurrencyVM;

            var icon = await currencyVM.LoadIcon();

            if (ct.IsCancellationRequested) return;

            iconImage.sprite = icon;
            amountText.SetTextParams(new(currencyVM.AmountStr.CurrentValue));
        }
    }
}