using AD.Services.Router;
using Game.Inventory;
using Game.UI;

namespace Game.Instances
{
    public class ConsumablesItemVM : ItemVM, IStackableItemVM
    {
        private readonly ConsumablesItemInfo info;

        public ItemStackVM StackVM { get; }
        public UIStateVM StackableStateVM { get; }

        public new ConsumablesDataVM DataVM { get; }

        public ConsumablesItemVM(ConsumablesItemInfo info, InstancesVMFactory instancesVMF)
            : base(info, instancesVMF.InventoryVMF)
        {
            this.info = info;

            StackVM = new(info.Stack);
            StackableStateVM = new();

            DataVM = instancesVMF.InventoryVMF.CreateItemData(DataId) as ConsumablesDataVM;
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