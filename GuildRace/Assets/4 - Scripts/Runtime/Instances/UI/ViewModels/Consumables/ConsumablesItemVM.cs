using AD.Services.Localization;
using AD.Services.Router;
using Game.Inventory;
using Game.UI;

namespace Game.Instances
{
    public class ConsumablesItemVM : ItemVM, IStackableItemVM
    {
        private readonly ConsumablesItemInfo info;
        private readonly ConsumableMechanicVM mechanicVM;

        public Rarity Rarity { get; }
        public LocalizeKey DescKey { get; }

        public ItemStackVM StackVM { get; }
        public UIStateVM StackableStateVM { get; }

        public ConsumablesItemVM(ConsumablesItemInfo info, InstancesVMFactory instancesVMF, InventoryVMFactory inventoryVMF)
            : base(info, inventoryVMF)
        {
            this.info = info;

            mechanicVM = instancesVMF.GetConsumableMechanic(info.MechanicId);

            Rarity = info.Rarity;
            DescKey = info.DescKey;

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