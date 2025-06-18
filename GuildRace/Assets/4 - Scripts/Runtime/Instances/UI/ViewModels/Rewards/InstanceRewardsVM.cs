using System.Collections.Generic;
using AD.Services.Router;

namespace Game.Instances
{
    public class InstanceRewardsVM : VMCollection<InstanceRewardData, InstanceRewardVM>
    {
        private readonly InstancesVMFactory instancesVMF;

        public InstanceRewardsVM(IReadOnlyCollection<InstanceRewardData> values, InstancesVMFactory instancesVMF) : base(values)
        {
            this.instancesVMF = instancesVMF;
        }

        protected override InstanceRewardVM Create(InstanceRewardData value)
        {
            return new InstanceRewardVM(value, instancesVMF);
        }
    }
}