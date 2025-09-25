using System;

namespace Game.Instances
{
    public class ReagentRewardVMFactory : RewardVMFactory
    {
        public override Type Type { get; } = typeof(ReagentRewardHandler);

        public override RewardMechanicVM GetMechanic(InstanceRewardData data, RewardHandler handler, InstancesVMFactory instancesVMF)
        {
            return new ReagentRewardMechanicVM(data, handler as ReagentRewardHandler, instancesVMF);
        }

        public override RewardResultVM GetResult(RewardResult info, InstancesVMFactory instancesVMF)
        {
            return new ReagentRewardResultVM(info as ReagentRewardResult, instancesVMF);
        }
    }
}