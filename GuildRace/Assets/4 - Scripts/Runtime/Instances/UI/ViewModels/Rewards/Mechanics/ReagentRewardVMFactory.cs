using System;

namespace Game.Instances
{
    public class ReagentRewardVMFactory : RewardVMFactory
    {
        public override Type Type { get; } = typeof(ReagentRewardHandler);

        public override RewardMechanicVM GetValue(InstanceRewardData data, RewardHandler handler, InstancesVMFactory instancesVMF)
        {
            return new ReagentRewardMechanicVM(data, handler as ReagentRewardHandler, instancesVMF);
        }
    }
}