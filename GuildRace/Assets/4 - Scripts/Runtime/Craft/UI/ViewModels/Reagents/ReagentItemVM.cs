using AD.Services.Router;
using Game.Inventory;
using Game.UI;

namespace Game.Craft
{
    public class ReagentItemVM : ItemVM, IStackableItemVM
    {
        private readonly ReagentItemInfo info;

        public ItemStackVM StackVM { get; }
        public UIStateVM StackableStateVM { get; }

        public new ReagentDataVM DataVM { get; }

        public ReagentItemVM(ReagentItemInfo info, InventoryVMFactory inventoryVMF) : base(info, inventoryVMF)
        {
            this.info = info;

            StackVM = new(info.Stack);
            StackableStateVM = new();

            DataVM = inventoryVMF.CreateItemData(info.DataId) as ReagentDataVM;
        }

        protected override void InitSubscribes()
        {
            base.InitSubscribes();

            DataVM.AddTo(this);
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