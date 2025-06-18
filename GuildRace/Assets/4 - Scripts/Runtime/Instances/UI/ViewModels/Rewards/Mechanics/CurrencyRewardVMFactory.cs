using System;

namespace Game.Instances
{
    public class CurrencyRewardVMFactory : RewardVMFactory
    {
        public override Type Type { get; } = typeof(CurrencyRewardHandler);

        public override RewardMechanicVM GetValue(InstanceRewardData data, RewardHandler handler, InstancesVMFactory instancesVMF)
        {
            return new CurrencyRewardMechanicVM(data, handler as CurrencyRewardHandler, instancesVMF);
        }
    }
}