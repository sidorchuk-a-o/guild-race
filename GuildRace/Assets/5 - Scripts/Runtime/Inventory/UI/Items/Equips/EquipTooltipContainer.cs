using System.Threading;
using AD.ToolsCollection;
using AD.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AD.Game.Inventory
{
    public class EquipTooltipContainer : ItemTooltipContainer
    {
        [Header("Equips")]
        [SerializeField] private UIText levelText;
        [Space]
        [SerializeField] private EquipGroupItem equipGroupItem;
        [SerializeField] private EquipTypeItem equipTypeItem;
        [SerializeField] private ItemSlotItem slotItem;
        [Space]
        [SerializeField] private RarityComponent rarityComponent;
        [SerializeField] private TooltipItemStackComponent stackComponent;

        public override async UniTask Init(TooltipContext context, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(context, disp, ct);

            if (ct.IsCancellationRequested) return;

            var dataVM = (EquipDataVM)context.DataVM;

            levelText.SetTextParams(dataVM.Level);

            slotItem.Init(dataVM.SlotVM);
            equipTypeItem.Init(dataVM.TypeVM);
            rarityComponent.Init(dataVM.RarityVM);
            stackComponent.Init(context, disp);

            await equipGroupItem.Init(dataVM.GroupVM, ct);
        }
    }
}