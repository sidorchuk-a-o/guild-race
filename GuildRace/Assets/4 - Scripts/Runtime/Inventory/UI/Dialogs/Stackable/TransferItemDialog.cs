using UnityEngine;

namespace Game.Inventory
{
    public class TransferItemDialog : StackableItemDialog
    {
        protected override int GetMaxSize()
        {
            var hoveredItemVM = Context.HoveredItemVM;
            var hoveredStackableVM = hoveredItemVM as IStackableItemVM;
            var hoveredStackVM = hoveredStackableVM.StackVM;

            var transferCount = Mathf.Min(
                a: base.GetMaxSize(),
                b: hoveredStackVM.AvailableSpace);

            return transferCount;
        }

        protected override void OkCallback()
        {
            var selectedItemVM = Context.SelectedItemVM;
            var hoveredItemVM = Context.HoveredItemVM;

            InventoryVMF.TryTransferItem(new TransferItemArgs
            {
                SourceItemId = selectedItemVM.Id,
                TargetItemId = hoveredItemVM.Id,
                Count = Size
            });

            base.OkCallback();
        }
    }
}