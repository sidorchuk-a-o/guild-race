using System;

namespace Game.Instances
{
    public class EquipRewardVMFactory : RewardVMFactory
    {
        public override Type Type { get; } = typeof(EquipRewardHandler);

        public override RewardMechanicVM GetMechanic(InstanceRewardData data, RewardHandler handler, InstancesVMFactory instancesVMF)
        {
            return new EquipRewardMechanicVM(data, handler as EquipRewardHandler, instancesVMF);
        }

        public override RewardResultVM GetResult(RewardResult info, InstancesVMFactory instancesVMF)
        {
            return new EquipRewardResultVM(info as EquipRewardResult, instancesVMF);
        }
    }
}