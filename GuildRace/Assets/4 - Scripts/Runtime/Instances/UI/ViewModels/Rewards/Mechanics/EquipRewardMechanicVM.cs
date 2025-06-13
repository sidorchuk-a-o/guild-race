using AD.Services.Router;
using Game.Inventory;

namespace Game.Instances
{
    public class EquipRewardMechanicVM : RewardMechanicVM
    {
        public EquipDataVM EquipVM { get; }

        public EquipRewardMechanicVM(InstanceRewardData data, EquipRewardHandler handler, InstancesVMFactory instancesVMF) 
            : base(data, handler, instancesVMF)
        {
            var itemId = handler.GetEquipId(data);
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