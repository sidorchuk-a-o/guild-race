using System.Threading;
using AD.UI;
using Cysharp.Threading.Tasks;
using Game.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Instances
{
    public class ReagentRewardItemContent : RewardItemContent
    {
        [Header("Equip")]
        [SerializeField] private Image iconImage;
        [SerializeField] private Image rarityImage;
        [SerializeField] private UIText countText;

        [Header("Tooltip")]
        [SerializeField] private InventoryTooltipComponent tooltipComponent;

        public override async UniTask Init(InstanceRewardVM rewardVM, CancellationTokenSource ct)
        {
            var mechanicVM = rewardVM.MechanicVM as ReagentRewardMechanicVM;
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