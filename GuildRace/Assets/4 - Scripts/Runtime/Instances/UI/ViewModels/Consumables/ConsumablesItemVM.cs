using AD.Services.Localization;
using AD.Services.Router;
using Game.Inventory;
using Game.UI;

namespace Game.Instances
{
    public class ConsumablesItemVM : ItemVM, IStackableItemVM
    {
        private readonly ConsumablesItemInfo info;

        public LocalizeKey DescKey { get; }
        public RarityDataVM RarityVM { get; }
        public ConsumableMechanicVM MechanicVM { get; }

        public ItemStackVM StackVM { get; }
        public UIStateVM StackableStateVM { get; }

        public ConsumablesItemVM(ConsumablesItemInfo info, InstancesVMFactory instancesVMF)
            : base(info, instancesVMF.InventoryVMF)
        {
            this.info = info;

            DescKey = info.DescKey;

            MechanicVM = instancesVMF.GetConsumableMechanic(info);
            RarityVM = instancesVMF.InventoryVMF.GetRarity(info.Rarity);
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