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
        [SerializeField] private EquipGroupItem equipGroupItem;
        [SerializeField] private EquipTypeItem equipTypeItem;
        [SerializeField] private ItemSlotItem slotItem;

        public override async UniTask Init(ItemVM itemVM, CompositeDisp disp, CancellationTokenSource ct)
        {
            await base.Init(itemVM, disp, ct);

            if (ct.IsCancellationRequested) return;

            var equipVM = itemVM as EquipItemVM;

            levelText.SetTextParams(equipVM.Level);

            slotItem.Init(equipVM.SlotVM);
            equipTypeItem.Init(equipVM.TypeVM);

            await equipGroupItem.Init(equipVM.GroupVM, ct);
        }
    }
}