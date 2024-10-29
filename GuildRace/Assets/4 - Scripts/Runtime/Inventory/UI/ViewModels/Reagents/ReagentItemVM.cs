using AD.ToolsCollection;

namespace Game.Inventory
{
    public class ReagentItemVM : ItemVM, IStackableItemVM
    {
        private readonly ReagentItemInfo info;

        public ItemStackVM Stack { get; }
        public UIStateVM StackableState { get; }

        public ReagentItemVM(ReagentItemInfo info, InventoryVMFactory inventoryVMF) : base(info, inventoryVMF)
        {
            this.info = info;

            Stack = new(info.Stack);
            StackableState = new();
        }

        protected override void InitSubscribes(CompositeDisp disp)
        {
            base.InitSubscribes(disp);

            Stack.AddTo(disp);
            StackableState.AddTo(disp);
        }

        // == IVMStackableItem ==

        public bool CheckPossibilityOfSplit()
        {
            return info.CheckPossibilityOfSplit();
        }

        public bool CheckPossibilityOfTransfer(ItemVM itemVM)
        {
            return false;
            //return inventoryVMF.CheckPossibilityOfTransfer(new TransferItemArgs
            //{
            //    SourceItemId = itemVM.Id,
            //    TargetItemId = info.Id
            //});
        }
    }
}