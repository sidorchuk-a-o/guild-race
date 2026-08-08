using AD.Game.Craft;
using AD.Router;

namespace AD.Game.Instances
{
    public class ReagentRewardMechanicVM : RewardMechanicVM
    {
        public int Count { get; }
        public ReagentDataVM ReagentVM { get; }

        public ReagentRewardMechanicVM(InstanceRewardData data, ReagentRewardHandler handler, InstancesVMFactory instancesVMF)
            : base(data, handler, instancesVMF)
        {
            var reagentId = ReagentRewardHandler.GetReagentId(data);
            var reagentDataVM = instancesVMF.InventoryVMF.CreateItemData(reagentId);

            Count = ReagentRewardHandler.GetReagentCount(data);
            ReagentVM = reagentDataVM as ReagentDataVM;
        }

        protected override void InitSubscribes()
        {
            base.InitSubscribes();

            ReagentVM.AddTo(this);
        }
    }
}