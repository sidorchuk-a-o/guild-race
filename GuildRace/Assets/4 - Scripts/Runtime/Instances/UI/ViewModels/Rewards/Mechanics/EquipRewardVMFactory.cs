using System;

namespace Game.Instances
{
    public class EquipRewardVMFactory : RewardVMFactory
    {
        public override Type Type { get; } = typeof(EquipRewardHandler);

        public override RewardMechanicVM GetValue(InstanceRewardData data, RewardHandler handler, InstancesVMFactory instancesVMF)
        {
            return new EquipRewardMechanicVM(data, handler as EquipRewardHandler, instancesVMF);
        }
    }
}