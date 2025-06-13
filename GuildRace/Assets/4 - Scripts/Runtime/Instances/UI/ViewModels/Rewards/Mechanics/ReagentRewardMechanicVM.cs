using AD.Services.Router;
using Game.Craft;

namespace Game.Instances
{
    public class ReagentRewardMechanicVM : RewardMechanicVM
    {
        public ReagentDataVM ReagentVM { get; }
        public int Count { get; }

        public ReagentRewardMechanicVM(InstanceRewardData data, ReagentRewardHandler handler, InstancesVMFactory instancesVMF)
            : base(data, handler, instancesVMF)
        {
            var reagentId = handler.GetReagentId(data);
            var reagentDataVM = instancesVMF.InventoryVMF.CreateItemData(reagentId);

            Count = handler.GetReagentCount(data);
            ReagentVM = reagentDataVM as ReagentDataVM;
        }

        protected override void InitSubscribes()
        {
            base.InitSubscribes();

            ReagentVM.AddTo(this);
        }
    }
}