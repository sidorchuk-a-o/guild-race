using System;
using AD.Router;

namespace AD.Game.Instances
{
    public class EquipRewardVMFactory : VMFactory, IRewardVMFactory
    {
        public Type Type { get; } = typeof(EquipRewardHandler);

        public RewardMechanicVM GetMechanic(InstanceRewardData data, RewardHandler handler, InstancesVMFactory instancesVMF)
        {
            return new EquipRewardMechanicVM(data, handler as EquipRewardHandler, instancesVMF);
        }

        public RewardResultVM GetResult(RewardResult info, InstancesVMFactory instancesVMF)
        {
            return new EquipRewardResultVM(info as EquipRewardResult, instancesVMF);
        }
    }
}