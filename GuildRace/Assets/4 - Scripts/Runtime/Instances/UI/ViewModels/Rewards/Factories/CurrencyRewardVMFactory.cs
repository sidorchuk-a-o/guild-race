using System;

namespace Game.Instances
{
    public class CurrencyRewardVMFactory : RewardVMFactory
    {
        public override Type Type { get; } = typeof(CurrencyRewardHandler);

        public override RewardMechanicVM GetMechanic(InstanceRewardData data, RewardHandler handler, InstancesVMFactory instancesVMF)
        {
            return new CurrencyRewardMechanicVM(data, handler as CurrencyRewardHandler, instancesVMF);
        }

        public override RewardResultVM GetResult(RewardResult info, InstancesVMFactory instancesVMF)
        {
            return new CurrencyRewardResultVM(info as CurrencyRewardResult, instancesVMF);
        }
    }
}