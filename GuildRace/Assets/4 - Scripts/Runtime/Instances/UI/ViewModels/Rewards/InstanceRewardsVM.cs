using System.Collections.Generic;
using AD.Services.Router;

namespace Game.Instances
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
            return new InstanceRewardVM(value, instancesVMF);
        }
    }
}