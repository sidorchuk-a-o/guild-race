using System;
using AD.Router;

namespace AD.Game.Instances
{
    public class CurrencyRewardVMFactory : VMFactory, IRewardVMFactory
    {
        public Type Type { get; } = typeof(CurrencyRewardHandler);

        public RewardMechanicVM GetMechanic(InstanceRewardData data, RewardHandler handler, InstancesVMFactory instancesVMF)
        {
            return new CurrencyRewardMechanicVM(data, handler as CurrencyRewardHandler, instancesVMF);
        }

        public RewardResultVM GetResult(RewardResult info, InstancesVMFactory instancesVMF)
        {
            return new CurrencyRewardResultVM(info as CurrencyRewardResult, instancesVMF);
        }
    }
}