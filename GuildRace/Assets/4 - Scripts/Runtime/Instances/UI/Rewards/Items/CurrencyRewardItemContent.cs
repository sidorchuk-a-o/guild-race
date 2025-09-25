using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using AD.UI;

namespace Game.Instances
{
    public class CurrencyRewardItemContent : RewardItemContent
    {
        [Header("Currency")]
        [SerializeField] private Image iconImage;
        [SerializeField] private UIText amountText;

        public override async UniTask Init(InstanceRewardVM rewardVM, CancellationTokenSource ct)
        {
            var resultVM = rewardVM.ResultVM as CurrencyRewardResultVM;
            var mechanicVM = rewardVM.MechanicVM as CurrencyRewardMechanicVM;
            var currencyVM = resultVM?.CurrencyVM ?? mechanicVM.CurrencyVM;

            var icon = await currencyVM.LoadIcon();

            if (ct.IsCancellationRequested) return;

            iconImage.sprite = icon;
            amountText.SetTextParams(new(currencyVM.AmountStr.Value));
        }
    }
}