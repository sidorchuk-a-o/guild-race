using System;
using AD.ToolsCollection;

namespace Game.Instances
{
    public abstract class RewardVMFactory : ScriptableData
    {
        public abstract Type Type { get; }

        public abstract RewardMechanicVM GetValue(InstanceRewardData data, RewardHandler handler, InstancesVMFactory instancesVMF);
    }
}