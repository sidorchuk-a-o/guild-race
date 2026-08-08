using System.Threading;
using AD.Game.Inventory;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace AD.Game.Instances
{
    public class EquipRewardItemContent : RewardItemContent
    {
        [Header("Equip")]
        [SerializeField] private Image iconImage;
        [SerializeField] private Image rarityImage;

        [Header("Tooltip")]
        [SerializeField] private InventoryTooltipComponent tooltipComponent;

        public override async UniTask Init(InstanceRewardVM rewardVM, CancellationTokenSource ct)
        {
            var mechanicVM = (EquipRewardMechanicVM)rewardVM.MechanicVM;
            var equipVM = mechanicVM.EquipVM;

            var icon = await equipVM.LoadIcon(ct);

            if (ct.IsCancellationRequested) return;

            iconImage.sprite = icon;
            rarityImage.color = equipVM.RarityVM.Color;

            tooltipComponent.Init(equipVM);
        }
    }
}