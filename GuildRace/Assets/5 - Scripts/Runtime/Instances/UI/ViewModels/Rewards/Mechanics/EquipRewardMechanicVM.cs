using AD.Game.Inventory;
using AD.Router;

namespace AD.Game.Instances
{
    public class EquipRewardMechanicVM : RewardMechanicVM
    {
        public EquipDataVM EquipVM { get; }

        public EquipRewardMechanicVM(InstanceRewardData data, EquipRewardHandler handler, InstancesVMFactory instancesVMF)
            : base(data, handler, instancesVMF)
        {
            var itemId = EquipRewardHandler.GetEquipId(data);
            var itemDataVM = instancesVMF.InventoryVMF.CreateItemData(itemId);

            EquipVM = itemDataVM as EquipDataVM;
        }

        protected override void InitSubscribes()
        {
            base.InitSubscribes();

            EquipVM.AddTo(this);
        }
    }
}