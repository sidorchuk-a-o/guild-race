using System.Collections.Generic;
using AD.Router;

namespace AD.Game.Instances
{
    public class InstanceRewardsVM : VMCollection<InstanceRewardInfo, InstanceRewardVM>
    {
        private readonly InstancesVMFactory instancesVMF;

        public InstanceRewardsVM(IReadOnlyCollection<InstanceRewardInfo> values, InstancesVMFactory instancesVMF) : base(values)
        {
            this.instancesVMF = instancesVMF;
        }

        protected override InstanceRewardVM Create(InstanceRewardInfo value)
        {
            return new(value, instancesVMF);
        }
    }
}