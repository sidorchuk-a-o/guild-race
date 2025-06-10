using AD.Services.Localization;
using AD.Services.Router;
using Game.Inventory;

namespace Game.Instances
{
    public class ConsumablesDataVM : ItemDataVM
    {
        public LocalizeKey DescKey { get; }

        public RarityDataVM RarityVM { get; }
        public ConsumableMechanicVM MechanicVM { get; }

        public ConsumablesDataVM(ConsumablesItemData data, InstancesVMFactory instancesVMF)
            : base(data, instancesVMF.InventoryVMF)
        {
            DescKey = data.DescKey;

            RarityVM = instancesVMF.InventoryVMF.GetRarity(data.Rarity);
            MechanicVM = instancesVMF.GetConsumableMechanic(data);
        }

        protected override void InitSubscribes()
        {
            base.InitSubscribes();

            RarityVM.AddTo(this);
            MechanicVM.AddTo(this);
        }
    }
}