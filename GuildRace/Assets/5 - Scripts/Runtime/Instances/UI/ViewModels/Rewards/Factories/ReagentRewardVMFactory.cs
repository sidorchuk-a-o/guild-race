using System;
using AD.Router;

namespace AD.Game.Instances
{
    public class ReagentRewardVMFactory : VMFactory, IRewardVMFactory
    {
        public Type Type { get; } = typeof(ReagentRewardHandler);

        public RewardMechanicVM GetMechanic(InstanceRewardData data, RewardHandler handler, InstancesVMFactory instancesVMF)
        {
            return new ReagentRewardMechanicVM(data, handler as ReagentRewardHandler, instancesVMF);
        }

        public RewardResultVM GetResult(RewardResult info, InstancesVMFactory instancesVMF)
        {
            return new ReagentRewardResultVM(info as ReagentRewardResult, instancesVMF);
        }
    }
}