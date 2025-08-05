using System.Threading;
using AD.Services.Store;
using AD.UI;
using Cysharp.Threading.Tasks;
using Game.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Store
{
    public class ReagentRewardItemContent : RewardItemContent
    {
        [Header("Equip")]
        [SerializeField] private Image iconImage;
        [SerializeField] private Image rarityImage;
        [SerializeField] private UIText countText;

        [Header("Tooltip")]
        [SerializeField] private InventoryTooltipComponent tooltipComponent;

        public override async UniTask Init(RewardResultVM rewardVM, CancellationTokenSource ct)
        {
            var mechanicVM = rewardVM as ReagentRewardResultVM;
            var reagentVM = mechanicVM.ReagentVM;

            var icon = await reagentVM.LoadIcon(ct);

            if (ct.IsCancellationRequested) return;

            iconImage.sprite = icon;
            rarityImage.color = reagentVM.RarityVM.Color;
            countText.SetTextParams(mechanicVM.Count);

            tooltipComponent.Init(reagentVM);
        }
    }
}