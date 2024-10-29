using UnityEngine;

namespace Game.Inventory
{
    public class TransferItemDialog : StackableItemDialog
    {
        protected override int GetMaxSize()
        {
            var hoveredItem = Context.HoveredItem;
            var hoveredStackable = hoveredItem as IStackableItemVM;
            var hoveredStack = hoveredStackable.Stack;

            var transferCount = Mathf.Min(
                a: base.GetMaxSize(),
                b: hoveredStack.AvailableSpace);

            return transferCount;
        }

        protected override void OkCallback()
        {
            var selectedItem = Context.SelectedItem;
            var hoveredItem = Context.HoveredItem;

            InventoryVMF.TryTransferItem(new TransferItemArgs
            {
                SourceItemId = selectedItem.Id,
                TargetItemId = hoveredItem.Id,
                Count = Size
            });

            base.OkCallback();
        }
    }
}