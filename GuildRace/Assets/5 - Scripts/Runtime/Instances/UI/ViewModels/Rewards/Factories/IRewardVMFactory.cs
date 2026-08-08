using System;

namespace AD.Game.Instances
{
    public interface IRewardVMFactory
    {
        Type Type { get; }

        RewardMechanicVM GetMechanic(InstanceRewardData data, RewardHandler handler, InstancesVMFactory instancesVMF);
        RewardResultVM GetResult(RewardResult info, InstancesVMFactory instancesVMF);
    }
}