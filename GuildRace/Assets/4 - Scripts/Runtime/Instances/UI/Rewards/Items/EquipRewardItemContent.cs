using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Instances
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
            var mechanicVM = rewardVM.MechanicVM as EquipRewardMechanicVM;
            var equipVM = mechanicVM.EquipVM;

            var icon = await equipVM.LoadIcon(ct);

            if (ct.IsCancellationRequested) return;

            iconImage.sprite = icon;
            rarityImage.color = equipVM.RarityVM.Color;

            tooltipComponent.Init(equipVM);
        }
    }
}