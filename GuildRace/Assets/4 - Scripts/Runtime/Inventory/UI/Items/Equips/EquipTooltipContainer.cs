using System.Threading;
using Cysharp.Threading.Tasks;
using AD.UI;
using AD.ToolsCollection;
using UnityEngine;

namespace Game.Inventory
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

            var dataVM = context.DataVM as EquipDataVM;

            levelText.SetTextParams(dataVM.Level);

            slotItem.Init(dataVM.SlotVM);
            equipTypeItem.Init(dataVM.TypeVM);
            rarityComponent.Init(dataVM.RarityVM);
            stackComponent.Init(context, disp);

            await equipGroupItem.Init(dataVM.GroupVM, ct);
        }
    }
}