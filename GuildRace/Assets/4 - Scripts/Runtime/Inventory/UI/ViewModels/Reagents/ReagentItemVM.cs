using AD.Services.Router;
using Game.UI;

namespace Game.Inventory
{
    public class ReagentItemVM : ItemVM, IStackableItemVM
    {
        private readonly ReagentItemInfo info;

        public ItemStackVM StackVM { get; }
        public UIStateVM StackableStateVM { get; }

        public ReagentItemVM(ReagentItemInfo info, InventoryVMFactory inventoryVMF) : base(info, inventoryVMF)
        {
            this.info = info;

            StackVM = new(info.Stack);
            StackableStateVM = new();
        }

        protected override void InitSubscribes()
        {
            base.InitSubscribes();

            StackVM.AddTo(this);
            StackableStateVM.AddTo(this);
        }

        // == IVMStackableItem ==

        public bool CheckPossibilityOfSplit()
        {
            return info.CheckPossibilityOfSplit();
        }

        public bool CheckPossibilityOfTransfer(ItemVM itemVM)
        {
            return inventoryVMF.CheckPossibilityOfTransfer(new TransferItemArgs
            {
                SourceItemId = itemVM.Id,
                TargetItemId = info.Id
            });
        }
    }
}