using System.Threading;
using AD.Game.Inventory;
using AD.Store;
using AD.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace AD.Game.Store
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
            var mechanicVM = (ReagentRewardResultVM)rewardVM;
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