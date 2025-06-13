using System.Threading;
using AD.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Instances
{
    public class CurrencyRewardItemContent : RewardItemContent
    {
        [Header("Currency")]
        [SerializeField] private Image iconImage;
        [SerializeField] private UIText amountText;

        public override async UniTask Init(InstanceRewardVM rewardVM, CancellationTokenSource ct)
        {
            var mechanicVM = rewardVM.MechanicVM as CurrencyRewardMechanicVM;
            var currencyVM = mechanicVM.CurrencyVM;

            var icon = await currencyVM.LoadIcon();

            if (ct.IsCancellationRequested) return;

            iconImage.sprite = icon;
            amountText.SetTextParams(new(currencyVM.Amount.Value));
        }
    }
}